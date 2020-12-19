using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeed.Models
{
    public class NewsViewModel
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
    }
}
