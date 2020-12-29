using NewsFeed.Models;
using System.Collections.Generic;
using System.Linq;

namespace NewsFeed.Repository
{
    public class CategoryRepo : ICategoryRepository
    {
        private readonly NewsContext context;
        public CategoryRepo(NewsContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return context.Categories;
        }

        public Category GetCategory(int id)
        {
            return context.Categories.FirstOrDefault(c => c.Id == id);
        }
    }
}
