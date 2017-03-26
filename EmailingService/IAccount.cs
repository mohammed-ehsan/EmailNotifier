using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailingService
{
    public interface IAccount
    {
        string Username { get; set; }
        string Password { get; set; }
        string SMTPServer { get; set; }
        string IMAPServer { get; set; }
        int SMTPPort { get; set; }
        int IMAPPort { get; set; }

    }
}
