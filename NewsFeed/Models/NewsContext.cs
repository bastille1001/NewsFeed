using Microsoft.EntityFrameworkCore;


namespace NewsFeed.Models
{
    public class NewsContext : DbContext
    {
        public DbSet<News> News { get; set; }

        public NewsContext(DbContextOptions<NewsContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
