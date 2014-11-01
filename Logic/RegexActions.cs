using EmailScraper.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace EmailScraper.Logic
{
    public class RegexActions : IDisposable
    {
        public List<WebsiteToScrape> GetRegexForWebsites(string theContent, string theRegex)
        {
            MatchCollection m1 = GetRegex(theContent, theRegex);
            List<WebsiteToScrape> results = new List<WebsiteToScrape>();

            foreach (Match m in m1)
            {
                results.Add(new WebsiteToScrape
                {
                    WebAddress = "http://www.odv-zb.si/" + m.Groups[1].ToString()
                });
            }

            return results;
        }

        public List<ScrapeDetail> GetRegexForValues(string theContent, string theRegex)
        {
            MatchCollection m1 = GetRegex(theContent, theRegex);
            List<ScrapeDetail> emails = new List<ScrapeDetail>();


            foreach (Match m in m1)
            {
                emails.Add(new ScrapeDetail {
                   Email =  m.Groups[2].ToString()
                });
            }

            return emails;
        }

        private static MatchCollection GetRegex(string theContent, string theRegex)
        {
            MatchCollection m1 = Regex.Matches(theContent, theRegex, RegexOptions.None);
            return m1;
        }

        public void Dispose()
        {
            //this.Dispose();
        }
    }
}