using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Steam.Data;
using Steam.Data.Entities;
using Steam.Models.News;

namespace Steam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly AppEFContext _context;

        public NewsController(AppEFContext context)
        {
            _context = context;
        }

        // Переглянути список новин
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.News.ToListAsync());
        }

        // Найти новину за id
        [HttpGet("find/{id}")]
        public async Task<IActionResult> Find(int? id)
        {
            var newsEntity = await _context.News
                .Include(n => n.Game)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (newsEntity == null)
            {
                return NotFound();
            }

            var model = new NewsItemViewModel
            {
                Id = newsEntity.Id,
                Title = newsEntity.Title,
                Description = newsEntity.Description,
                DateOfRelease = newsEntity.DateOfRelease,
                Image = newsEntity.Image,
                VideoURL = newsEntity.VideoURL,
                GameId = newsEntity.GameId
            };

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewsCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newsEntity = new NewsEntity
                {
                    Title = model.Title,
                    Description = model.Description,
                    DateOfRelease = model.DateOfRelease,
                    Image = model.Image,
                    VideoURL = model.VideoURL,
                    GameId = model.GameId
                };

                _context.News.Add(newsEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(model);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var newsEntity = await _context.News.FindAsync(id);
            if (newsEntity == null)
            {
                return NotFound();
            }

            var model = new NewsEditViewModel
            {
                Id = newsEntity.Id,
                Title = newsEntity.Title,
                Description = newsEntity.Description,
                DateOfRelease = newsEntity.DateOfRelease,
                Image = newsEntity.Image,
                VideoURL = newsEntity.VideoURL,
                GameId = newsEntity.GameId
            };

            return Ok(model);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, NewsEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var newsEntity = await _context.News.FindAsync(id);
                if (newsEntity == null)
                {
                    return NotFound();
                }

                newsEntity.Title = model.Title;
                newsEntity.Description = model.Description;
                newsEntity.DateOfRelease = model.DateOfRelease;
                newsEntity.Image = model.Image;
                newsEntity.VideoURL = model.VideoURL;
                newsEntity.GameId = model.GameId;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(model);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var newsEntity = await _context.News.FindAsync(id);
            if(newsEntity == null)
            {
                return NotFound();
            }

            _context.News.Remove(newsEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
