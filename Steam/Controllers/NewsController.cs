using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Steam.Data;

namespace Steam.Controllers
{
    public class NewsController : Controller
    {
        private readonly AppEFContext _context;

        public NewsController(AppEFContext context)
        {
            _context = context;
        }

        // Переглянути список новин
        public async Task<IActionResult> Index()
        {
            return View(await _context.News.ToListAsync());
        }

        // Найти новину за id
        public async Task<IActionResult> Find(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(x => x.Id == id);
            if(news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // Створення новини (GET)
        public IActionResult Create() 
        {
            return View();
        }

        //// Створення новини (POST)
        //[HttpPost]
        //[ValidateAntiForgeryToken]
    }
}
