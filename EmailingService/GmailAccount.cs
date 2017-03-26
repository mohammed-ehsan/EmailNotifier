using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailingService
{
    public class GmailAccount : Account
    {
        public GmailAccount(string Username, string Password) :base()
        {
            this.Username = Username;
            this.Password = Password;
            IMAPPort = 993;
            SMTPServer = "smtp.gmail.com";
            IMAPServer = "imap.gmail.com";
            SMTPPort = 465;
        }
    }
}
