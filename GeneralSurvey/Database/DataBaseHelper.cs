using GeneralSurvey.Models;
using System.Data.SQLite;

namespace GeneralSurvey.Database
{
    public class DataBaseHelper
    {
        private SQLiteConnection? _connection;   
        public string ConnectionString { get; set; } = "Data Source=Database/GeneralSurvey.db;Version=3;";

        public void ConnectToDataBase()
        {
            _connection = new SQLiteConnection(ConnectionString);
            _connection.Open();
        }

        public SQLiteDataReader ExecuteQuery(string query)
        {
            using var cmd = new SQLiteCommand(query, _connection);

            return cmd.ExecuteReader();
        }

        public virtual void PostUser(User user)
        {
            var query = $"INSERT INTO User (username, password, salt) VALUES ('{user.Username}', '{user.Password}', '{user.Salt}')";
            ExecuteQuery(query);

            query = $"SELECT id FROM User WHERE username = '{user.Username}'";
            var reader = ExecuteQuery(query);
            reader.Read();
            user.Id = reader.GetInt32(0);
        }

        public void PostAPIKey(Guid apiKey)
        {
            var query = $"INSERT INTO API_KEY (key) VALUES ('{apiKey}')";
            ExecuteQuery(query);
        }

        public virtual void PutAPIKey(int userId, Guid apiKey)
        {
            var query = $"UPDATE API_KEY SET id_user = '{userId}' WHERE key = '{apiKey}'";
            ExecuteQuery(query);
        }

        public virtual User? GetUserByID(int id)
        {
            var query = $"SELECT * FROM User WHERE id = {id}";

            var reader = ExecuteQuery(query);
            if (!reader.HasRows)
                return new User();

            reader.Read();
            var user = new User
            {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                Password = reader.GetString(2)
            };

            return user;
        }

        public List<User> GetAllUsers()
        {
            var query = "SELECT * FROM User";
            var reader = ExecuteQuery(query);  

            var users = new List<User>();

            while (reader.Read())
            {
                var user = new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2)
                };

                users.Add(user);
            }

            return users;
        }

        public virtual bool VerifyAPIKey(Guid apiKey)
        {
            var query = $"SELECT key, id_user FROM API_KEY WHERE key = '{apiKey}'";
            var reader = ExecuteQuery(query);

            if (!reader.HasRows)
            {
                return false;
            }

            // Verify if the API key is already used
            reader.Read();

            return reader[reader.GetOrdinal("id_user")] == DBNull.Value;
        }

        public virtual List<User> GetUsersByUsername(string username)
        {
            var query = $"SELECT * FROM User WHERE username = '{username}'";
            var reader = ExecuteQuery(query);

            var users = new List<User>();

            while (reader.Read())
            {
                var user = new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    Salt = reader.GetString(3)
                };

                users.Add(user);
            }

            return users;
        }

        public void PostAnswers(SurveyResponse surveyResponse)
        {
            foreach (var answer in surveyResponse.QuestionAnswers)
            {
                var query = "INSERT INTO ANSWER (Id_Choice, Id_Survey) VALUES (@IdChoice, @IdSurvey)";

                using var cmd = new SQLiteCommand(query, _connection);

                cmd.Parameters.AddWithValue("@IdChoice", answer.ChoiceId);
                cmd.Parameters.AddWithValue("@IdSurvey", surveyResponse.SurveyId);

                cmd.ExecuteNonQuery();
            }
        }

        public bool PostUserAnswerSurvey(SurveyResponse surveyResponse)
        {
            if (HasUserAlreadyAnswered(surveyResponse.SurveyId, surveyResponse.UserId))
                return false;

            if (ValidateChoices(surveyResponse))
            {
                PostAnswers(surveyResponse);
                PostUserSurvey(surveyResponse.SurveyId, surveyResponse.UserId);
                return true;

            }

            return false;
        }

        public void PostUserSurvey(int surveyId, int userId)
        {
            var query = $"INSERT INTO SURVEY_USER (id_survey, id_user) VALUES (@IdSurvey, @IdUser)";

            using var cmd = new SQLiteCommand(query, _connection);

            cmd.Parameters.AddWithValue("@IdSurvey", surveyId);
            cmd.Parameters.AddWithValue("@IdUser", userId);

            cmd.ExecuteNonQuery();
        }

        public bool ValidateChoices(SurveyResponse surveyResponse)
        {
            var query = $"SELECT * FROM CHOICE WHERE id_question IN (SELECT id FROM QUESTION WHERE id_survey = {surveyResponse.SurveyId})";
            var reader = ExecuteQuery(query);

            if (!reader.HasRows)
                return false;

            var choices = new List<int>();
            while (reader.Read()) 
            {
                choices.Add(reader.GetInt32(0));
            }

            foreach (var answer in surveyResponse.QuestionAnswers)
            {
                if (!choices.Contains(answer.ChoiceId))
                {
                    return false;
                }
            }

            return true;
        }

        public bool HasUserAlreadyAnswered(int surveyId, int userId)
        {
            var query = $"SELECT * FROM SURVEY_USER WHERE id_survey = {surveyId} AND id_user = {userId}";
            var reader = ExecuteQuery(query);

            return reader.HasRows;
        }

        public virtual Survey? GetSurveyById(int id)
        {
            var query = $"SELECT * FROM SURVEY WHERE id = {id}";
            var reader = ExecuteQuery(query);

            if (!reader.HasRows)
                return null;

            reader.Read();

            var survey = new Survey
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1)

            };

            var questions = GetQuestionsBySurveyId(survey.Id);

            for (int i = 0; i < questions.Count; i++)
            {
                var choicesToAdd = GetChoicesByQuestionId(questions.ToList()[i].Id);
                questions.ToList()[i].Choices = choicesToAdd;
            }

            survey.Questions = questions;

            return survey;
        }

        public ICollection<Question> GetQuestionsBySurveyId(int surveyId)
        {
            var query = $"SELECT * FROM QUESTION WHERE id_survey = {surveyId}";
            var reader = ExecuteQuery(query);

            var questions = new List<Question>();

            if (!reader.HasRows)
                return questions;


            while (reader.Read())
            {
                var question = new Question
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                };
                questions.Add(question);
            }

            return questions;
        }

        public ICollection<Choice> GetChoicesByQuestionId(int questionId)
        {
            var query = $"SELECT * FROM CHOICE WHERE id_question = {questionId}";
            var reader = ExecuteQuery(query);

            var choices = new List<Choice>();

            while (reader.Read())
            {
                var choice = new Choice
                {
                    Id = reader.GetInt32(0),
                    Letter = reader.GetString(1),
                    Response = reader.GetString(3)
                };
                choices.Add(choice);
            }

            return choices;
        }

        public List<Answer> GetAnwsersBySurveyId(int surveyId)
        {
            var query = $"SELECT * FROM ANSWER WHERE id_survey = {surveyId}";
            var reader = ExecuteQuery(query);

            var answers = new List<Answer>();

            while (reader.Read())
            {
                var answer = new Answer
                {
                    choiceId = reader.GetInt32(1),
                };
                answers.Add(answer);
            }

            return answers;
        }

        public void CloseConnection()
        {
            _connection?.Close();
        }
    }
}
