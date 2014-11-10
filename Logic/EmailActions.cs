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
                        Debug.Write("Exception caught while trying to send an email - {0}", ex.Message);
                    }
                }
            }
        }

        public EmailMessage PrepareEmail(string email)
        {
            return new EmailMessage {
                MailTo = new List<string>
                    {email}.ToList(),
                MailFrom = "martinprogrammer@outlook.com",
                Content = @"
Postovani Ales,
 <br><br>
Kontaktiram vas u vezi kupovine nekretnine u Sloveniji I zanima me da li ste u poziciji da me zastupate u vezi ovakve transakcije.
Trenutno pregovaramo sa vlasnikom kuce u blizini Bohinjskog jezera, cenu I uslove prodaje, I uskoro ce mi biti potrebne usluge advokata, da bi se kupo-prodajna transakcija legalno kompletirala.
Zivim sa porodicom vec duze vremena u Engleskoj, sa srpskim pasosem I trajnom/neogranicenom dozvolom rada I boravka u Britaniji.
Nadam se da mozete potvrditi:<br />
<ul>
<li>Da li ste registrovani za ovakvu vrstu transakcija</li>
<li>Koji je process I koliko uobicajeno traje</li>
<li>Koja su mi dokumenta potrebna</li>
<li> je trosak (vase usluge + opstinske/drzavne/porezne)</li>
</ul>
 
 <br><br>
I’m contacting you regarding the purchase of a property in Slovenia, and would appreciate if you could help with such transaction.
We’re currently negotiating the price and sale conditions with the owner of the house near lake Bohinj,  and will soon require services of a solicitor in order to complete the legal side of the sale.
I live with my family in Englnd, have a Serbian passport and permanent leave to remain in Britain. (permanent visa)
I hope you will be able to confirm:<br />
<ul>
<li>Whether you specialize in this area of law</li>
<li>What is the procedure and how long it usually lasts</li>
<li>What documents are needed  from the buyer’s side</li>
<li>What is the cost (both your services + council/tax/other admin)</li>
 </ul>
 <br><br>
Srdacan pozdrav,
Martin
                ",

                 Subject = "Kupovina hise"
            };



        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}