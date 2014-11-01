using EmailScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace EmailScraper.Logic
{
    public class EmailActions
    {
        public void SendEmail(EmailMessage theMessage)
        {
            foreach (string sendTo in theMessage.MailTo)
            {
                MailMessage myEmail = new MailMessage(
                    theMessage.MailFrom,
                    sendTo,
                    theMessage.Subject,
                    theMessage.Content
                    );

                using (SmtpClient myClient = new SmtpClient())
                {
                    try
                    {
                        myClient.Send(myEmail);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception caught while trying to send an email - {0}", ex.Message);
                    }
                }
            }
        }
    }
}