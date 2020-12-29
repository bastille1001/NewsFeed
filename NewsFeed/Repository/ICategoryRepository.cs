using NewsFeed.Models;
using System.Collections.Generic;


namespace NewsFeed.Repository
{
    interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
    }
}
