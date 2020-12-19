using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsFeed.Models;

namespace NewsFeed.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _repo;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsController(INewsRepository repo, IWebHostEnvironment webHostEnvironment)
        {
            _repo = repo;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _repo.GetAllNews();
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = _repo.GetAllNews()
                .FirstOrDefault(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Description,Name,NewsId,Image")] NewsViewModel nm)
        {
            if (ModelState.IsValid)
            {
                string fileName = UploadFile(nm);
                var news = new News
                {
                    Image = fileName,
                    Description = nm.Description,
                    Name = nm.Name
                };
                _repo.Create(news);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private string UploadFile(NewsViewModel nm)
        {
            string fileName = null;
            if(nm.Image != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                fileName = Guid.NewGuid().ToString() + "-" + nm.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using(var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    nm.Image.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var news = _repo.ReadNews(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Description,Name,NewsId,Image")] News news)
        {
            if (id != news.NewsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(news);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.NewsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var news = _repo.ReadNews(id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var news = _repo.ReadNews(id);
            _repo.Delete(news.NewsId);
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _repo.GetAllNews().Any(n => n.NewsId == id);
        }
    }
}
