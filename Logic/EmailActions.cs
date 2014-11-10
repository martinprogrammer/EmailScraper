using EmailScraper.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace EmailScraper.Logic
{
    public class EmailActions : IDisposable
    {
        public void SendEmail(EmailMessage theMessage)
        {

            SmtpClient myClient;
            myClient = new SmtpClient("smtp-mail.outlook.com");

            myClient.Host = "smtp-mail.outlook.com";
             myClient.Port = 587;

            myClient.UseDefaultCredentials = false;
            myClient.Credentials = new System.Net.NetworkCredential("");
            myClient.Timeout = 100000;
            myClient.EnableSsl = true;



            foreach (string sendTo in theMessage.MailTo)
            {
                MailMessage myEmail = new MailMessage(
                    theMessage.MailFrom,
                    sendTo,
                    theMessage.Subject,
                    theMessage.Content
                                    );

                myEmail.IsBodyHtml = true;
                myEmail.CC.Add(new MailAddress(""));

                try
                {
                    myClient.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                    myClient.Send(myEmail);
                }
                catch (Exception ex)
                {
                    Debug.Write("Exception caught while trying to send an email - {0}", ex.Message);
                }

            }
        }

        void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled == true || e.Error != null)
            {
                throw new Exception(e.Cancelled ? "Email sending was canceled." : "Error: " + e.Error.ToString());
            }
        }

        public EmailMessage PrepareEmail(string email)
        {
            return new EmailMessage
            {
                MailTo = new List<string> { email }.ToList(),
                MailFrom = "",
                Content = @"
                ",

                Subject = ""
            };



        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}