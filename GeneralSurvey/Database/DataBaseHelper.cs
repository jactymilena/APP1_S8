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
            var query = $"INSERT INTO User (username, password, salt) VALUES (@username, @password, @salt)";
            using var cmd = new SQLiteCommand(query, _connection);

            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@salt", user.Salt);

            cmd.ExecuteNonQuery();

            query = $"SELECT id FROM User WHERE username = @username";
            using var cmd2 = new SQLiteCommand(query, _connection);
            cmd2.Parameters.AddWithValue("@username", user.Username);

            var reader = cmd2.ExecuteReader();
            reader.Read();
            user.Id = reader.GetInt32(0);
        }

        public void PostAPIKey(Guid apiKey)
        {
            var query = $"INSERT INTO API_KEY (key) VALUES (@apiKey)";
            using var cmd = new SQLiteCommand(query, _connection);

            cmd.Parameters.AddWithValue("@apiKey", apiKey);
            cmd.ExecuteNonQuery();
        }

        public virtual void PutAPIKey(int userId, Guid apiKey)
        {
            var query = $"UPDATE API_KEY SET id_user = @userId WHERE key = @apiKey";
            using var cmd = new SQLiteCommand(query, _connection);

            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@apiKey", apiKey.ToString());
            cmd.ExecuteNonQuery();

        }

        public virtual User? GetUserByID(int id)
        {
            var query = $"SELECT * FROM User WHERE id = @id";
            using var cmd = new SQLiteCommand(query, _connection);

            cmd.Parameters.AddWithValue("@id", id);

            var reader = cmd.ExecuteReader();
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

        public virtual bool VerifyAPIKey(Guid apiKey)
        {
            var query = $"SELECT key, id_user FROM API_KEY WHERE key = @apikey";
            using var cmd = new SQLiteCommand(query, _connection);

            cmd.Parameters.AddWithValue("@apikey", apiKey.ToString());

            var reader = cmd.ExecuteReader();

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
            var query = $"SELECT * FROM User WHERE username = @username";
            using var cmd = new SQLiteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@username", username);

            var reader = cmd.ExecuteReader();
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
            var query = $"SELECT * FROM CHOICE WHERE id_question IN (SELECT id FROM QUESTION WHERE id_survey = @id_survey)";
            using var cmd = new SQLiteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@id_survey", surveyResponse.SurveyId);

            var reader = cmd.ExecuteReader();

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
            var query = $"SELECT * FROM SURVEY_USER WHERE id_survey = @surveyId AND id_user = @userId";
            using var cmd = new SQLiteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@surveyId", surveyId);
            cmd.Parameters.AddWithValue("@userId", userId);

            var reader = cmd.ExecuteReader();

            return reader.HasRows;
        }

        public virtual Survey? GetSurveyById(int id)
        {
            var query = $"SELECT * FROM SURVEY WHERE id = @id";
            using var cmd = new SQLiteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@id", id);

            var reader = cmd.ExecuteReader();

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
            var query = $"SELECT * FROM QUESTION WHERE id_survey = @surveyId";
            using var cmd = new SQLiteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@surveyId", surveyId);

            var reader = cmd.ExecuteReader();

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
            var query = $"SELECT * FROM CHOICE WHERE id_question = @questionId";
            using var cmd = new SQLiteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@questionId", questionId);

            var reader = cmd.ExecuteReader();

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
            var query = $"SELECT * FROM ANSWER WHERE id_survey = @surveyId";
            using var cmd = new SQLiteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@surveyId", surveyId);

            var reader = cmd.ExecuteReader();

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
