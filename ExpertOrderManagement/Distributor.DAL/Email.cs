using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distributor.DAL
{
    public class Email
    {
        public static bool SendMail(string to, string from, string subject, string body)
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

            mailMessage.To.Add(to);
            if (!string.IsNullOrEmpty(from))
                mailMessage.From = new System.Net.Mail.MailAddress(from);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = System.Net.Mail.MailPriority.High;

            //mailMessage.Attachments.Add(new Attachment(fileupload));
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception exc)
            {
                throw;
            }
        }
    }
}
