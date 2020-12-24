using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeed.Models
{
    public class NewsViewModel
    {
        public IEnumerable<News> AllNews { get; set; }
        public string CurrCategory { get; set; }
    }
}
