using EmailScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailScraper.Logic
{
    public class DatabaseActions : IDisposable
    {
        
        WebsiteContext myContext = new WebsiteContext();
        public void Dispose()
        {

            GC.SuppressFinalize(this);

        }

        public void AddWebPage(WebsiteToScrape websiteToScrape)
        {
            myContext.Websites.Add(websiteToScrape);
            myContext.SaveChanges();
        }

        public IQueryable<WebsiteToScrape> GetWebsites()
        {
            return myContext.Websites;
        }



        public void AddWebsites(List<WebsiteToScrape> theList)
        {
            foreach(WebsiteToScrape website in theList)
            {
                AddWebPage(website);
            }
        }

        public void AddEmail(ScrapeDetail theDetail)
        {
            myContext.PagesToScrape.Add(theDetail);
            myContext.SaveChanges();
        }

        public IEnumerable<string> GetEmails()
        {
            return myContext.PagesToScrape.Select(p => p.Email);
        }

    }
}