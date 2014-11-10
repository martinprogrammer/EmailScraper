using EmailScraper.Logic;
using EmailScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace EmailScraper.Controllers
{
    public class HomeController : AsyncController
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetWebsites()
        {
            string theUrl = @"http://www.odv-zb.si/imenik/imenik-odvetnikov/SearchFull?ime=&priimek=&posta=&kraj=&druzba=&p=4&jezik_Nem%u0161ki=false&jezik_%u0160panski=false&jezik_Hrva%u0161ki=false&jezik_Holandski=false&jezik_Bosanski=false&jezik_Srbski=false&jezik_Ruski=false&jezik_Makedonski=false&jezik_Francoski=false&jezik_Angle%u0161ki=false&jezik_Srbohrva%u0161ki=false&jezik_Italijanski=false&jezik_Mad%u017earski=false&jezik_Latinski=false&podrocje_Civilno=false&podrocje_Ustavno=false&podrocje_Kazensko=false&podrocje_Gospodarsko=false&podrocje_Upravno=false&podrocje_Mednarodno=false&podrocje_Delovno=false&podrocje_PravoEU=false&podrocje_Medijsko=false&podrocje_Insolvencno=false&podrocje_Davcno=false&podrocje_Socvarnost=false&podrocje_Intelektlastnina=false&podrocje_Spec=false&zbor_Celje=false&zbor_Koper=false&zbor_Kranj=true&zbor_Kranj=false&zbor_Kr%u0161ko=false&zbor_Ljubljana=false&zbor_Maribor=false&zbor_Pomurje=false&zbor_Nova+Gorica=false&zbor_Novo+mesto=false&zbor_Ptuj=false&zbor_Slovenj+Gradec=false&Search=I%u0161%u010di&cookieguard_allowedCookies";
            string content = "";
            List<WebsiteToScrape> theList;

            using (HtmlActions myHtml = new HtmlActions())
            {
                content = await myHtml.DownloadFromWeb(theUrl);
            }

            using (RegexActions theRegex = new RegexActions())
            {
                theList = theRegex.GetRegexForWebsites(content, @"NavigateTo\('(.*?)'");
                using (DatabaseActions dbActions = new DatabaseActions())
                {
                    dbActions.AddWebsites(theList);
                }
            }

            ViewBag.NumberOfWebsites = theList.Count;
            return View(theList);
        }


        public async Task<ActionResult> GetEmails()
        {
            ScrapeWebsite myScrape = new ScrapeWebsite();
            List<Task<string>> EmailPages = myScrape.GetEmails();
            List<ScrapeDetail> CleanEmails = new List<ScrapeDetail>();

            var theEmailPages = await Task.WhenAll(EmailPages);

            using (RegexActions myRegex = new RegexActions())
            {
                foreach (var page in theEmailPages)
                {
                    List<ScrapeDetail> theEmails = myRegex.GetRegexForValues(page, "(mailto:)(\\S+@\\S+)\"");
                    CleanEmails.AddRange(theEmails);
                    CleanEmails.RemoveAll(p => p.Email == "info@odv-zb.si");
                }
            }


            using (DatabaseActions dBActions = new DatabaseActions())
            {
                CleanEmails.ForEach(p => { dBActions.AddEmail(p); });
            }



            ViewBag.NumberOfEmails = CleanEmails.Count;
            return View(CleanEmails);

        }

        public ActionResult SendEmails()
        {


            using (DatabaseActions myDb = new DatabaseActions())
            {
                var emails = myDb.GetEmails();
                using (EmailActions myEmail = new EmailActions())
                {
                    foreach (var email in emails)
                    {
                        myEmail.SendEmail(myEmail.PrepareEmail(email));
                    }
                }
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}