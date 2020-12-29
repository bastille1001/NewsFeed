using System.Collections.Generic;


namespace NewsFeed.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
