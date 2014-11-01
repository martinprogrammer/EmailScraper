using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmailScraper.Models
{
    public class WebsiteContext : DbContext
    {
        public DbSet<WebsiteToScrape> Websites { get; set; }
        public DbSet<ScrapeDetail> PagesToScrape { get; set; }
    }
}