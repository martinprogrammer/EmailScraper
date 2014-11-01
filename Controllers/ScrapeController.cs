using EmailScraper.Logic;
using EmailScraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmailScraper.Controllers
{
    public class ScrapeController : Controller
    {
        // GET: Scrape
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Scrape()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Scrape(WebsiteToScrape theWebsite)
        {
            using(ScrapeWebsite myScrape = new ScrapeWebsite())
            {
                var result = myScrape.GetTheScrapes(theWebsite.WebAddress);
            }
            return View();
        }

        public ActionResult GetWebsites()
        {
            using(DatabaseActions myActions = new DatabaseActions())
            {
                return View(myActions.GetWebsites());
            }
           
        }


    }
}