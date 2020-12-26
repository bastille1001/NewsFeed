using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeed.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
