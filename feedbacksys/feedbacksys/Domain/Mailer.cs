using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace feedbacksys.Domain
{
    public class Mailer
    {
        public void SendMail()
        {

            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential("Active\\bli2", "1+7=8lipeng");   // User with 'sendAs' permissions on the mailbox. Cannot use your own creds
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("Kun.Yan@activenetwork.com");   // From this mailbox..

            smtpClient.Host = "outlooklas.active.local";
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;

            message.From = fromAddress;
            message.Subject = "test in c#";
            //Set IsBodyHtml to true means you can send HTML email.
            message.IsBodyHtml = true;
            message.Body = "<h1>Message BODY</h1>";
            message.To.Add("Brevin.Li@activenetwork.com");

            try
            {
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                //Error, could not send the message
                Console.WriteLine(ex.Message);
                Console.WriteLine("Finished");
                Console.ReadLine();
            } 
        }
    }
}