using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmailScraper.Models
{
    public class EmailMessage
    {
        [DataType(DataType.EmailAddress)]
        public string MailFrom { get; set; }
        [DataType(DataType.EmailAddress)]
        public IEnumerable<string> MailTo { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}