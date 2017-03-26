using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AE.Net.Mail;
using AE.Net.Mail.Imap;
using System.Net.Mail;

namespace EmailingService
{
    public class Account : IAccount
    {
        public string IMAPServer { get; set; }
        public string Password { get; set; }
        public int SMTPPort { get; set; }
        public int IMAPPort { get; set; }
        public string SMTPServer { get; set; }
        public string Username { get; set; }

        public Account()
        {
             
        }
    }
}
