using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Email_Notification.Interfaces;
namespace DatabaseManagementService
{
    public class User : IUser
    {
        public string NickName { get; set; }
        

        public string Password { get; set; }
        

        public string Username { get; set; }

        public User()
        {

        }

        public User(string nickname, string username, string password)
        {
            this.Username = username;
            this.NickName = nickname;
            this.Password = password;
        }

       
        public Object CreateInstance(string nickname, string username, string password)
        {
            User us = new User(nickname,username,password);
            return us;
        }
    }
}
