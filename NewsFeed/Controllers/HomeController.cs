using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsFeed.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeed.Controllers
{
    public class HomeController : Controller
    {
        NewsContext db;

        public HomeController(NewsContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View(db.News.ToList());
        }
    }
}
