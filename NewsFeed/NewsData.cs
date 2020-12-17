using NewsFeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeed
{
    public static class NewsData
    {
        public static void Initialize(NewsContext context)
        {
            if (!context.News.Any())
            {
                context.News.AddRange(
                    new News
                    {
                        Name = "Facebook Invests $150 Million in Affordable Housing for the Bay Area",
                        Description = "We’re designating $150 million of our $1 billion affordable housing commitment to the Bay Area’s " +
                        "lowest-income residents.",
                        Image = "/img/mark.jpg"
                    },
                    new News
                    {
                        Name = "Nagorno-Karabakh conflict flares despite ceasefire",
                        Description = "Two men are accused of mutilating the bodies of Armenian soldiers in the Nagorno-Karabakh region.",
                        Image = "/img/karabakh.jpg"
                    },
                    new News
                    {
                        Name = "Microsoft releases native Office apps for M1 Macs",
                        Description = "Collectively tagged as Microsoft 365 for Mac Apps, the company's core Office productivity " +
                        "applications have been updated for Apple's new ARM-based laptops.",
                        Image = "/img/micro.jpg"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
