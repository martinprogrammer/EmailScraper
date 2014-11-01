using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmailScraper.Models
{
    public class ScrapeDetail
    {
        [Key]
        public int ScrapeDetailID { get; set; }
        public int WebsiteToScrapeID {get; set;}
        public virtual WebsiteToScrape WebsiteDetails { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
    }
}