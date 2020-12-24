using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeed.Models
{
    public class SQLNewsRepository : INewsRepository
    {
        private readonly NewsContext context;
        public SQLNewsRepository(NewsContext context)
        {
            this.context = context;
        }


        public News Create(News news)
        {
            
            context.News.Add(news);
            context.SaveChanges();
            return news;
        }

        public News Delete(int id)
        {
            News news = context.News.Find(id);
            if(news != null)
            {
                context.News.Remove(news);
                context.SaveChanges();
            }
            return news;
        }

        public IEnumerable<News> GetAllNews()
        {
            return context.News;
        }

        public News ReadNews(int id)
        {
            return context.News.Find(id);
        }

        public News Update(News news)
        {
            var n = context.News.Attach(news);
            n.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return news;
        }
    }
}
