using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmailingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AE.Net.Mail.Imap;
using AE.Net.Mail;

namespace EmailingService.Tests
{
    [TestClass()]
    public class EmailManagerTests
    {
        [TestMethod()]
        public async Task GetlastMessageTest()
        {
            GmailAccount account = new GmailAccount("developer1engineer@gmail.com", "ubtsxrwpqnvscxlp");

            MailManager ma = new MailManager(account);
            ma.InitializeIMAPConnection();
            MailMessage ms = await ma.GetLastMessage();
            Console.WriteLine(ms.Body);
        }
    }
}