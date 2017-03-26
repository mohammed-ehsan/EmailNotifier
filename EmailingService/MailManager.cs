using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AE.Net.Mail.Imap;
using AE.Net.Mail;
using System.Net.Mail;
using System.Net;

namespace EmailingService
{
    public class MailManager : IDisposable
    {
        /// <summary>
        /// IAccount object represents account data for connection.
        /// </summary>
        public IAccount Account { get; set; }
        public ImapClient IMAPClient { get; private set; }

        /// <summary>
        /// returns number of retries if connection is failed due to any reason, default value 5 retries.
        /// </summary>
        public int RetryCountLimit { get; set; }

        /// <summary>
        /// Delay between retries in second, default value 10 seconds.
        /// </summary>
        public int Delay { get; set; }

        public MailManager(IAccount Account)
        {
            this.Account = Account;
            IMAPClient = new ImapClient();
            RetryCountLimit = 5;
            Delay = 10;
        }

        public void InitializeIMAPConnection()
        {
            if (!IMAPClient.IsConnected)
            {
                IMAPClient = Task.Run<ImapClient>(() => getIMAPClient()).Result;
            }
        }

        int retry = 0;
        /// <summary>
        /// Get fully initialized IMAPClient object from the Account property.
        /// </summary>
        /// <returns></returns>
        private async Task<ImapClient> getIMAPClient()
        {
            try
            {
                ImapClient cli = new ImapClient(Account.IMAPServer, Account.Username, Account.Password, AuthMethods.Login, Account.IMAPPort, true, false);
                return cli;
            }
            catch (Exception ex)
            {
                if (retry < RetryCountLimit)
                {
                    retry++;
                    System.Threading.Thread.Sleep(Delay * 1000);
                    return await getIMAPClient();
                }
                throw ex;
            }
        }

        /// <summary>
        /// gets SmtpClient object fully initialized from the Account proprty.
        /// </summary>
        /// <returns>Returns SmtpClient</returns>
        private SmtpClient getSMTPClient()
        {
            SmtpClient cli = new SmtpClient(Account.SMTPServer);
            cli.Credentials = new NetworkCredential(Account.Username, Account.Password);
            cli.DeliveryMethod = SmtpDeliveryMethod.Network;
            cli.EnableSsl = true;
            cli.Port = Account.SMTPPort;
            return cli;
        }
        

        /// <summary>
        /// Gets the last message asyncronously.
        /// </summary>
        /// <returns></returns>
        public async Task<AE.Net.Mail.MailMessage> GetLastMessage()
        {
            ImapClient cli = await getIMAPClient();
            try
            {
                int messgaeCount = cli.GetMessageCount();
                AE.Net.Mail.MailMessage lastMessage = cli.GetMessage(messgaeCount - 1,false,false);
                return lastMessage;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Gets the message in the specified index asyncronously.
        /// </summary>
        /// <param name="index">message index.</param>
        /// <returns></returns>
        public async Task<AE.Net.Mail.MailMessage> GetMessage(int index)
        {
            ImapClient cli = await getIMAPClient();
            try
            {
                AE.Net.Mail.MailMessage message = cli.GetMessage(index);
                return message;
            }
            catch
            {
                throw;
            }
        }

        public void Dispose()
        {
            IMAPClient.Dispose();
        }
    }
}
