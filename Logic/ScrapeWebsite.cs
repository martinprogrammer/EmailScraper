using EmailScraper.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace EmailScraper.Logic
{
    public class ScrapeWebsite : IDisposable
    {
       public IEnumerable<ScrapeDetail> GetTheScrapes(string theWebsite)
        {
            using (HtmlActions myHtml = new HtmlActions())
            {
                var theContent = myHtml.DownloadFromWeb(theWebsite).ToString();
                ProcessWebPage(theContent);
            }

            return new List<ScrapeDetail>();
        }

        private void ProcessWebPage(string theContent)
        {
            List<WebsiteToScrape> theList = new List<WebsiteToScrape>();
            Match theMatch = Regex.Match(theContent, @"NavigateTo\('(.*)'");
            theMatch = theMatch.NextMatch();
            theMatch = theMatch.NextMatch();
            MatchCollection m1 = Regex.Matches(theContent, @"NavigateTo\('(.*)'", RegexOptions.Singleline);
            using (DatabaseActions dbActions = new DatabaseActions())
            {
                foreach (Match m in m1)
                {
                    Debug.WriteLine(m.Groups[1].ToString());
                    //dbActions.AddWebPage(new WebsiteToScrape
                    //{

                    //    WebAddress = "http://www.odv-zb.si" + m.Groups[1].ToString()
                    //});
                }
            }
        }

        private void ProcessSubPages()
        {
            //IQueryable<WebsiteToScrape
        }






        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Task<string>> GetEmails()
        {
            List<WebsiteToScrape> websites;
            List<Task<string>> contentList = new List<Task<string>>();
            DatabaseActions dbActions;

            using (dbActions = new DatabaseActions())
            {

                websites = dbActions.GetWebsites().ToList();
                foreach (WebsiteToScrape website in websites)
                {
                    using (HtmlActions myHtml = new HtmlActions())
                    {
                        Task<string> myContent = myHtml.DownloadFromWeb(website.WebAddress);
                        contentList.Add(myContent);
                    }
                }
            }

            return contentList;

        }
    }
}