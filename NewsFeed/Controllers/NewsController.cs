using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsFeed.Models;

namespace NewsFeed.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsController(NewsContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.News.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.NewsId == id);
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
        public async Task<IActionResult> Create([Bind("Description,Name,NewsId,Image")] NewsViewModel nm)
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
                _context.Add(news);
                await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,Name,NewsId,Image")] News news)
        {
            if (id != news.NewsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
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


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.NewsId == id);
        }
    }
}
