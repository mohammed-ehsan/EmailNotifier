using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Security.AccessControl;
using Email_Notification.Interfaces;

namespace DatabaseManagementService
{
    public class SQLiteManager
    {
        public string ConnectionString { get; set; }
        public string applicationDataPath;
        public string databasePath;
        public SQLiteManager(string ConnectionString)
        {
            applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\EmailNotifier";
            databasePath = applicationDataPath + @"\Usersdatabase.sqlite3";
            this.ConnectionString = "Data Source= " + databasePath + "; Version=3; Password=mohammedehsanfadhel;";
            InstantiateDBFolder();
            InstantiateDBFiles();
        }

        private void InstantiateDBFolder()
        {
            DirectorySecurity security = new DirectorySecurity();
            security.SetAccessRule(new FileSystemAccessRule(Environment.UserName, FileSystemRights.Modify, AccessControlType.Allow));
            Directory.CreateDirectory(applicationDataPath, security);
        }

        private void InstantiateDBFiles()
        {
            if (!File.Exists(databasePath))
            {
                try
                {
                    SQLiteConnection.CreateFile(databasePath);
                    SQLiteConnection con = new SQLiteConnection(ConnectionString);
                    con.SetPassword("mohammedehsanfadhel");
                    con = null;
                    using (con = new SQLiteConnection(ConnectionString))
                    {
                        con.Open();
                        SQLiteCommand com = new SQLiteCommand("CREATE TABLE Users (id INTEGER PRIMARY KEY, Nickname NVARCHAR(150), Username VARCHAR(150), Password NVARCHAR(100))", con);
                        com.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public void AddUser(IUser user)
        {
            try
            {
                SQLiteCommand com = new SQLiteCommand("INSERT INTO Users (Nickname, Username, Password) VALUES (@nickname, @username, @password)");
                com.Parameters.Add(new SQLiteParameter("nickname", user.NickName));
                com.Parameters.Add(new SQLiteParameter("username", user.Username));
                com.Parameters.Add(new SQLiteParameter("password", user.Password));
                ExecuteNonQueryCommand(com);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteUser(string Username)
        {
            if (UserExists(Username))
            {
                SQLiteCommand com = new SQLiteCommand("DELETE FROM Users WHERE Username=@username");
                com.Parameters.Add(new SQLiteParameter("username", Username));
                ExecuteNonQueryCommand(com);
            }
        }

        public void UpdateUser(IUser user)
        {
            if (UserExists(user.Username))
            {
                SQLiteCommand com = new SQLiteCommand("UPDATE Users SET Nickname=@nickname, Password=@password WHERE Username=@username");
                com.Parameters.Add(new SQLiteParameter("username", user.Username));
                com.Parameters.Add(new SQLiteParameter("nickname", user.NickName));
                com.Parameters.Add(new SQLiteParameter("password", user.Password));
                ExecuteNonQueryCommand(com);
            }
        }

        public T GetUser<T>(string username) where T : IUser
        {
            if (UserExists(username))
            {
                SQLiteCommand com = new SQLiteCommand("SELECT * FROM Users WHERE Username=@username");
                com.Parameters.Add(new SQLiteParameter("username", username));
                using(SQLiteConnection con = getConnection())
                {
                    con.Open();
                    com.Connection = con;
                    SQLiteDataReader reader = com.ExecuteReader();
                    if (!reader.HasRows)
                        return default(T);
                    reader.Read();
                    T user = Activator.CreateInstance<T>();
                    user.NickName = (string)reader["Nickname"];
                    user.Username = (string)reader["Username"];
                    user.Password = (string)reader["Password"];
                    reader.Close();
                    return user;
                }
            }
            return default(T);
        }

        public bool UserExists(string Username)
        {
            SQLiteCommand com = new SQLiteCommand("SELECT * FROM Users WHERE Username=@username");
            com.Parameters.Add(new SQLiteParameter("username", Username));
            SQLiteConnection con = getConnection();
            com.Connection = con;
            try
            {
                con.Open();
                SQLiteDataReader reader = com.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public T[] GetUsers<T>() where T : IUser
        {
            SQLiteCommand com = new SQLiteCommand("SELECT * FROM Users");
            try
            {
                using (SQLiteConnection con = getConnection())
                {
                    con.Open();
                    com.Connection = con;
                    SQLiteDataReader reader = com.ExecuteReader();
                    if (!reader.HasRows)
                        return new T[0];
                    List<T> users = new List<T>();
                    while (reader.Read())
                    {
                        T User = (T)Activator.CreateInstance(typeof(T));
                        string username = (string)reader["Username"];
                        string nickname = (string)reader["Nickname"];
                        string password = (string)reader["Password"];
                        User.NickName = nickname;
                        User.Username = username;
                        User.Password = password;
                        users.Add(User);
                    }
                    return users.ToArray();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private SQLiteConnection getConnection()
        {
            SQLiteConnection con = new SQLiteConnection(ConnectionString);
            return con;
        }

        private void ExecuteNonQueryCommand(SQLiteCommand com)
        {
            try
            {
                using (SQLiteConnection con = getConnection())
                {
                    con.Open();
                    com.Connection = con;
                    com.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
