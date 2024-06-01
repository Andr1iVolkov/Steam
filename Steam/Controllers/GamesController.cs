using AutoMapper;
using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Steam.Data;
using Steam.Data.Entities;
using Steam.Models.Category;
using Steam.Models.Game;
using Steam.Models.News;

namespace Steam.Controllers
{
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly AppEFContext _context;
        private readonly IMapper _mapper;

        public GamesController(AppEFContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Переглянути список ігор
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            return Ok(await _context.Games
                .Include(g => g.GameCategories)
                .ThenInclude(gc => gc.Category)
                .Select(x => _mapper.Map<CategoryItemViewModel>(x))
                .ToListAsync());
        }

        // Найти ігру за id
        [HttpGet("find/{id}")]
        public async Task<IActionResult> Find(int? id)
        {
            var gameEntity = await _context.Games
                .Include(g => g.GameCategories)
                .ThenInclude (gc => gc.Category)
                .Include(g => g.GameImages)
                .Include(g => g.News)
                .FirstOrDefaultAsync(m => m.Id == id);

            if(gameEntity == null)
            {
                return NotFound();
            }

            var model = new GameItemViewModel
            {
                Id = gameEntity.Id,
                Name = gameEntity.Name,
                Price = gameEntity.Price,
                Description = gameEntity.Description,
                DateOfRelease = gameEntity.DateOfRelease,
                SystemRequirements = gameEntity.SystemRequirements,
                Categories = gameEntity.GameCategories.Select(gc => new Models.Category.CategoryItemViewModel
                {
                    Id = gc.Category.Id,
                    Name = gc.Category.Name
                }).ToList(),
                Images = gameEntity.GameImages.Select(gi => new GameImageViewModel
                {
                    Id = gi.Id,
                    Name = gi.Name,
                    Priority = gi.Priority
                }).ToList(),
                News = gameEntity.News.Select(n => new NewsItemViewModel
                {
                    Id = n.Id,
                    Title = n.Title,
                    Description = n.Description,
                    DateOfRelease = n.DateOfRelease,
                    Image = n.Image,
                    VideoURL = n.VideoURL
                }).ToList()

            };
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GameCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var gameEntity = new GameEntity
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    DateOfRelease = model.DateOfRelease,
                    SystemRequirements = model.SystemRequirements
                };

                _context.Add(gameEntity);
                await _context.SaveChangesAsync();

                if(model.SelectedCategoryIds != null)
                {
                    foreach(var categoryId in model.SelectedCategoryIds)
                    {
                        _context.GameCategory.Add(new GameCategoryEntity
                        {
                            GameId = gameEntity.Id,
                            CategoryId = categoryId
                        });
                    }
                }

                if(model.ImageUrls != null)
                {
                    foreach(var imageUrl in model.ImageUrls)
                    {
                        _context.GameImages.Add(new GameImageEntity
                        {
                            GameId = gameEntity.Id,
                            Name = imageUrl
                        });
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(model);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            var gameEntity = await _context.Games
                .Include(g => g.GameCategories)
                .Include(g => g.GameImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if(gameEntity == null)
            {
                return NotFound();
            }

            var model = new GameEditViewModel
            {
                Id = gameEntity.Id,
                Name = gameEntity.Name,
                Price = gameEntity.Price,
                Description = gameEntity.Description,
                DateOfRelease = gameEntity.DateOfRelease,
                SystemRequirements = gameEntity.SystemRequirements,
                SelectedCategoryIds = gameEntity.GameCategories.Select(gc => gc.CategoryId).ToList(),
                ImageUrls = gameEntity.GameImages.Select(gi => gi.Name).ToList()
            };

            return Ok(model);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int? id, GameEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var gameEntity = await _context.Games
                        .Include(g => g.GameCategories)
                        .Include(g => g.GameImages)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    if(gameEntity == null)
                    {
                        return NotFound();
                    }

                    gameEntity.Name = model.Name;
                    gameEntity.Price = model.Price;
                    gameEntity.Description = model.Description;
                    gameEntity.DateOfRelease = model.DateOfRelease;
                    gameEntity.SystemRequirements = model.SystemRequirements;

                    // Оновлення категорій 
                    var existingCategories = gameEntity.GameCategories.ToList();
                    _context.GameCategory.RemoveRange(existingCategories);

                   if(model.SelectedCategoryIds != null)
                   {
                        foreach(var categoryId in model.SelectedCategoryIds)
                        {
                            _context.GameCategory.Add(new GameCategoryEntity
                            {
                                GameId = gameEntity.Id,
                                CategoryId = categoryId
                            });
                        }
                   }

                    // Оновлення фото
                    var existingImages = gameEntity.GameImages.ToList();
                    _context.GameImages.RemoveRange(existingImages);

                    if(model.ImageUrls != null)
                    {
                        foreach (var imageUrl in model.ImageUrls)
                        {
                            _context.GameImages.Add(new GameImageEntity
                            {
                                GameId = gameEntity.Id,
                                Name = imageUrl
                            });
                        }
                    }

                    _context.Update(gameEntity);
                    await _context.SaveChangesAsync();
                }
                // Виняток, викликаний DbContext , коли очікувалося, що SaveChanges для
                // сутності призведе до оновлення бази даних, але насправді рядки в базі
                // даних не змінилися
                catch (DbUpdateConcurrencyException)
                {
                    if(!GameExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return Ok(model);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var gameEntity = await _context.Games.FirstOrDefaultAsync();
            if(gameEntity == null)
            {
                return NotFound();
            }

            _context.Games.Remove(gameEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
