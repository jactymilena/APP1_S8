using GeneralSurvey.Models;
using System.Data.SQLite;

namespace GeneralSurvey.Database
{
    public class DataBaseHelper
    {
        private SQLiteConnection? _connection;   
        public string ConnectionString { get; set; } = "Data Source=GeneralSurvey.db;Version=3;";

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
            var userTable = @"CREATE TABLE IF NOT EXISTS User (
                id INTEGER PRIMARY KEY NOT NULL,
                username CHAR(50) NOT NULL, 
                password CHAR(50) NOT NULL 
            )";
            
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

            ExecuteQuery(userTable);
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

        public Survey GetSurveyById(int id)
        {
            var query = $"SELECT * FROM Survey WHERE id = {id}";
            var reader = ExecuteQuery(query);
            reader.Read();

            var survey = new Survey
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1)
            };

            return survey;
        }

        public void CloseConnection()
        {
            _connection?.Close();
        }
    }
}
