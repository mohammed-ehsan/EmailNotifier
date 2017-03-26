using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SQLite;
using System.Data;
using Email_Notification.Interfaces;

namespace DatabaseManagementService.Tests
{
    [TestClass]
    public class SQLiteManagerTests
    {
        [TestMethod]
        public void InitializeDirectoryTest()
        {
            //Arrage
            SQLiteManager manager = new SQLiteManager(string.Empty);

            //Act
            manager.InstantiateDBFolder();

            //Assert
        }

        [TestMethod()]
        public void InstantiateDBFilesTests()
        {
            //Arrange
            SQLiteManager manager = new SQLiteManager(string.Empty);

            //Act
            manager.InstantiateDBFiles();
            SQLiteConnection con = new SQLiteConnection("Data Source= " + manager.databasePath + "; Version=3; Password=mohammedehsanfadhel");
            con.Open();
            con.Close();

            //assert
            
        }

        [TestMethod]
        public void AddUserTest()
        {
            string nickname = "Mohammed Ehsan";
            string username = "developer1engineer@gmail.com";
            string password = "somepassword";
            SQLiteManager ma = new SQLiteManager(string.Empty);
            ma.InstantiateDBFolder();
            ma.InstantiateDBFiles();
            ma.AddUser(new User(nickname, username, password));
        }

        [TestMethod]
        public void GetUsersTest()
        {
            SQLiteManager ma = new SQLiteManager(string.Empty);
            ma.InstantiateDBFolder();
            ma.InstantiateDBFiles();
            User[] users = ma.GetUsers<User>();
            foreach (User item in users)
            {
                Console.Write(item.NickName);
                Console.Write(", ");
                Console.Write(item.Username);
                Console.Write(", ");
                Console.Write(item.Password);
                Console.WriteLine();
            }
            
        }
        
    }
}
