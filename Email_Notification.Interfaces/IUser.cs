using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email_Notification.Interfaces
{
    public interface IUser
    {
        string Username { get; set; }
        string Password { get; set; }
        string NickName { get; set; }
    }
}
