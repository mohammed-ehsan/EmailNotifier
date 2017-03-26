using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Email_Notification.Interfaces;
using XmlHelperLibrary;
using System.Security.Cryptography;

namespace DatabaseManagementService
{
    class UsersManager
    {
        public List<IUser> Users { get; private set; }
        private SQLiteManager manager;
        public UsersManager()
        {
            Users = GetUsers();
            manager = new DatabaseManagementService.SQLiteManager(string.Empty);
            manager.InstantiateDBFolder();
            manager.InstantiateDBFiles();
        }

        /// <summary>
        /// Adds a new user to the users list, if user already exists it will be updated.
        /// </summary>
        /// <param name="user">User to be added.</param>
        public void AddUser(IUser user)
        {
            
            manager.AddUser(user);
        }

        public void DeleteUser(string username)
        {
            manager.DeleteUser(username);
        }

        public List<IUser> GetUsers()
        {
            List<IUser> result = new List<IUser>();

            //code to get the users list from database

            return result;
        }
        
        /// <summary>
        /// Check wheather a specific user exists using its Username property, if not return null.
        /// </summary>
        /// <param name="user">User to be checked.</param>
        /// <returns>IUser item match found.</returns>
        public IUser UserExists(IUser user)
        {
            foreach (IUser item in Users)
            {
                if (item.Username == user.Username)
                {
                    return item;
                }
            }
            return null;
        }
        
        /// <summary>
        /// Check wheather aa specific username exists, if not return null.
        /// </summary>
        /// <param name="Username">username to be checked.</param>
        /// <returns>Matching IUser object found or null if no match is found.</returns>
        public IUser UserExists(string Username)
        {
            foreach (IUser item in Users)
            {
                if (item.Username == Username)
                    return item;
            }
            return null;
        }
    }
}
