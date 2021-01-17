using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index(int page=1)
        {
            int pageSize = 3;

            var model = _repo.GetAll().ToList();
            var count = model.Count();
            var items = model.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel indexViewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                News = items
            };

            return View(indexViewModel);
        }

        public IActionResult ShowSearchForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShowSearchResults(string SearchPhrase)
        {
            return View("Index", _repo.GetAll().Where(n => n.Description.ToLower().Contains(SearchPhrase.ToLower())));
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = _repo.GetAll()
                .FirstOrDefault(m => m.Id == id);
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
        public IActionResult Create([Bind("Description,Name,Id,Image,Category")] News n)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(n.Image.FileName);
                string extension = Path.GetExtension(n.Image.FileName);
                n.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/img", fileName);
                using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    n.Image.CopyTo(fileStream);
                }
                _repo.Create(n);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return View(new News());
            }
            else
            {
                var news = _repo.Get((int)id);
                return View(news);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Description,Name,Id,Image")] News news)
        {
            if (news.Id > 0)
            {
                _repo.Update(news);
            }
            else
            {
                _repo.Create(news);
            }

            if(_repo.SaveChanges())
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(news);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var news = _repo.Get(id);
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
            var img = _repo.Get(id);
            
            //delete image from the wwwroot/img
            var imgPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", img.ImageName);
            if (System.IO.File.Exists(imgPath))
                System.IO.File.Delete(imgPath);

            //delete from database 
            var news = _repo.Get(id);
            _repo.Delete(news.Id);
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _repo.GetAll().Any(n => n.Id == id);
        }
    }
}
