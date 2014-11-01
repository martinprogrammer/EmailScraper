using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmailScraper.Models
{
    public class WebsiteToScrape
    {
        [Key]
        public int WebsiteToScrapeID { get; set; }
        [Required]
        [DataType(DataType.Html)]
        public string WebAddress { get; set; }
    }
}