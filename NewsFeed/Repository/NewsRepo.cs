﻿using System.Collections.Generic;
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

        public bool SaveChanges()
        {
            if (context.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public void Update(News news)
        {
            context.News.Update(news);
        }
    }
}
