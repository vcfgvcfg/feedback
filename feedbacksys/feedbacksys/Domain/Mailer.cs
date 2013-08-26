using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace feedbacksys.Domain
{
    public class TemplateHelper
    {
        /// <summary>
        /// 私有构造方法，不允许创建实例
        /// </summary>
        private TemplateHelper()
        {
            // TODO: Add constructor logic here
        }

        /// <summary>
        /// Template File Helper 
        /// </summary>
        /// <param name="templatePath">Templet Path</param>
        /// <param name="values">NameValueCollection</param>
        /// <returns>string</returns>
        public static string BulidByFile(string templatePath, NameValueCollection values)
        {
            return BulidByFile(templatePath, values, "[$", "]");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="values">NameValueCollection obj</param>
        /// <param name="prefix"></param>
        /// <param name="postfix"></param>
        /// <returns></returns>
        public static string Build(string template, NameValueCollection values, string prefix, string postfix)
        {
            if (values != null)
            {
                foreach (DictionaryEntry entry in values)
                {
                    template = template.Replace(string.Format("{0}{1}{2}", prefix, entry.Key, postfix),
                                                entry.Value.ToString());
                }
            }
            return template;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templatePath"></param>
        /// <param name="values"></param>
        /// <param name="prefix"></param>
        /// <param name="postfix"></param>
        /// <returns></returns>
        public static string BulidByFile(string templatePath, NameValueCollection values, string prefix, string postfix)
        {
            StreamReader reader = null;
            string template = string.Empty;
            try
            {
                reader = new StreamReader(templatePath);
                template = reader.ReadToEnd();
                reader.Close();
                if (values != null)
                {
                    foreach (string key in values.AllKeys)
                    {
                        template = template.Replace(string.Format("{0}{1}{2}", prefix, key, postfix), values[key]);
                    }
                }
            }
            catch
            {

            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return template;
        }
    }

    public class Mailer
    {
        public void SendMail(int id)
        {

            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential("Active\\bli2", "1+7=8lipeng");   // User with 'sendAs' permissions on the mailbox. Cannot use your own creds
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("Kun.Yan@activenetwork.com");   // From this mailbox..

            smtpClient.Host = "outlooklas.active.local";
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;

            string templetpath = @"E:\opensource\feedback\feedbacksys\feedbacksys\Content\mailtemplate\mail.html";
            NameValueCollection myCol = new NameValueCollection();
            myCol.Add("id", id.ToString());

          //  myCol.Add("link", System.Configuration.ConfigurationManager.AppSettings["rootUrl"]);

            message.From = fromAddress;
            message.Subject = "test in c#";
            //Set IsBodyHtml to true means you can send HTML email.
            message.IsBodyHtml = true;
            message.Body = TemplateHelper.BulidByFile(templetpath, myCol);
            message.To.Add("Brevin.Li@activenetwork.com");
            smtpClient.Send(message);

        }

     
    }
}