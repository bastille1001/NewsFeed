using System.Collections.Generic;

namespace NewsFeed.Models
{
    public interface INewsRepository 
    {
        News Get(int id);

        IEnumerable<News> GetAll();

        News Create(News t);

        News Delete(int id);

        bool SaveChanges();
        void Update(News news);
    }
}
