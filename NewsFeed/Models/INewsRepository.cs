using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace NewsFeed.Models
{
    public interface INewsRepository
    {
        News ReadNews(int id);

        IEnumerable<News> GetAllNews();

        News Create(News news);

        News Update(News news);

        News Delete(int id);
    }
}
