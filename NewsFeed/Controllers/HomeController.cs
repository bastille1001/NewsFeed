using Microsoft.AspNetCore.Mvc;
using NewsFeed.Models;
using System.Linq;

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


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(News news)
        {

            db.News.Add(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
