using NewsFeed.Models;
using System.Collections.Generic;


namespace NewsFeed.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
