﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
