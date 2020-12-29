using System.Collections.Generic;
using System.Linq;

namespace NewsFeed.Models
{
    public class NewsRepo : INewsRepository
    {
        private readonly NewsContext context;
        public NewsRepo(NewsContext context)
        {
            this.context = context;
        }

        public News Create(News t)
        {
            context.News.Add(t);
            context.SaveChanges();
            return t;
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

        public News Get(int id)
        {
            return context.News.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<News> GetAll()
        {
            return context.News;
        }

        public News Update(News t)
        {
            var result = context.News.FirstOrDefault(n => n.Id == t.Id);

            if(result != null)
            {
                result.Category = t.Category;
                result.Description = t.Description;
                result.Image = t.Image;
                result.ImageName = t.ImageName;
                result.Name = t.Name;
                

                context.SaveChanges();
                return result;
            }
            return null;

            /*var n = context.News.Attach(t);
            n.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return t;*/
        }
    }
}
