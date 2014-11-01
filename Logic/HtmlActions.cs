using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace EmailScraper.Logic
{
    public class HtmlActions : IDisposable
    {
        HttpClient theWeb = new HttpClient();

        public async Task<string> DownloadFromWeb(string theUrl)
        {
            return await theWeb.GetStringAsync(theUrl);
        }

        public void Dispose()
        {
            //theWeb = null;
            //this.Dispose();
        }
    }
}