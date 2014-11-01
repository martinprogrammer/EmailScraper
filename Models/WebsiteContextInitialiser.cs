using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EmailScraper.Models
{
    public class WebsiteContextInitialiser : DropCreateDatabaseIfModelChanges<WebsiteContext>
    {
        protected override void Seed(WebsiteContext context)
        {
            base.Seed(context);
        }
    }
}