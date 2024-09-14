﻿using GeneralSurvey.Models;
using System.Data.SQLite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GeneralSurvey.Database
{
    public class DataBaseHelper
    {
        private SQLiteConnection? _connection;   
        //public string ConnectionString { get; set; } = "Data Source=GeneralSurvey.db;Version=3;";
        public string ConnectionString { get; set; } = "Data Source=Database/database.db;Version=3;";


        public DataBaseHelper()
        {
            CreateDataBase();
        }

        private void CreateDataBase()
        {
            SQLiteConnection.CreateFile("GeneralSurvey.db");
            _connection = new SQLiteConnection(ConnectionString);
            _connection.Open();
        }

        public void CreateTables()
        {
            // ExecuteQuery("DROP TABLE IF EXISTS User");
            //var userTable = @"CREATE TABLE IF NOT EXISTS User (
            //    id INTEGER PRIMARY KEY NOT NULL,
            //    username CHAR(50) NOT NULL, 
            //    password CHAR(50) NOT NULL 
            //)";
            
            /*ExecuteQuery("DROP TABLE IF EXISTS Survey");
            var surveyTable = @"CREATE TABLE Survey (
                id INTEGER PRIMARY KEY NOT NULL, 
                title CHAR(50)
            );";

            ExecuteQuery("DROP TABLE IF EXISTS Question");
            var questionTable = @"CREATE TABLE Question (
                id INTEGER PRIMARY KEY NOT NULL, 
                title CHAR(50)
            );";*/

            //ExecuteQuery(userTable);
            // ExecuteQuery(surveyTable);
            // ExecuteQuery(questionTable);
        }

        public SQLiteDataReader ExecuteQuery(string query)
        {
            using var cmd = new SQLiteCommand(query, _connection);
            cmd.ExecuteNonQuery();

            return cmd.ExecuteReader();
        }

        public void PostUser(User user)
        {
            var query = $"INSERT INTO User (username, password) VALUES ('{user.Username}', '{user.Password}')";
            ExecuteQuery(query);
        }

        public User GetUserByID(int id)
        {
            var query = $"SELECT * FROM User WHERE id = {id}";

            var reader = ExecuteQuery(query);
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

        public void PostAnswers(ICollection<Answer> answers)
        {
            foreach (var answer in answers)
            {
                var query = "INSERT INTO ANSWER (Id, Id_Choice, Answer_Date) VALUES (@Id, @IdChoice, @AnswerDate)";

                using var cmd = new SQLiteCommand(query, _connection);

                cmd.Parameters.AddWithValue("@Id", (object)answer.Id ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IdChoice", answer.IdChoice ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@AnswerDate", answer.AnswerDate ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public Survey GetSurveyById(int id)
        {
            var query = $"SELECT * FROM SURVEY WHERE id = {id}";
            var reader = ExecuteQuery(query);
            reader.Read();

            var survey = new Survey
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1)
            };

            var questions = GetQuestionsBySurveyId(survey.Id);
            var choices = new List<Choice>();

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

            while (reader.Read())
            {
                var question = new Question
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    IdSurvey = reader.GetInt32(2)
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
                    IdQuestion = reader.GetInt32(2),
                    Response = reader.GetString(3)
                };
                choices.Add(choice);
            }

            return choices;
        }



        public void CloseConnection()
        {
            _connection?.Close();
        }
    }
}
