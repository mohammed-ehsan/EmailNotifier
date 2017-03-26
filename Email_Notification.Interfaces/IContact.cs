using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email_Notification.Interfaces
{
    public interface IContact
    {
        string Email { get; set; }
        string Nickname { get; set; }
    }
}
