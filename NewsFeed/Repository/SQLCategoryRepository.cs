using NewsFeed.Models;
using System.Collections.Generic;

namespace NewsFeed.Repository
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        
        private readonly NewsContext context;
        
        public SQLCategoryRepository(NewsContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> AllCategories => context.Categories;
    }
}
