using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeed.Models
{
    public class MockNewsRepository : INewsRepository
    {
        private readonly List<News> newsList;

        public MockNewsRepository()
        {
            newsList = new List<News>
            {
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
            };
        }

        public News Create(News news)
        {
            news.NewsId = newsList.Max(n => n.NewsId) + 1;
            newsList.Add(news);
            return news;
        }

        public News Delete(int id)
        {
            News news = newsList.FirstOrDefault(n => n.NewsId == id);
            if(news != null)
            {
                newsList.Remove(news);
            }
            return news;
        }

        public IEnumerable<News> GetAllNews()
        {
            return newsList;
        }

        public News ReadNews(int id)
        {
            return newsList.FirstOrDefault(n => n.NewsId == id); 
        }

        public News Update(News newsChanges)
        {
            News news = newsList.FirstOrDefault(n => n.NewsId == newsChanges.NewsId);
            if (news != null)
            {
                news.Name = newsChanges.Name;
                news.Description = newsChanges.Description;
                news.Image = newsChanges.Image;
            }
            return news;
        }
    }
}
