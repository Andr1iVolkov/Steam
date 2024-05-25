using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Steam.Data;
using Steam.Data.Entities;
using Steam.Models.Category;

namespace Steam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly AppEFContext _appEFContext;
        private readonly IMapper _mapper;

        public CategoryController(AppEFContext appEFContext, IMapper mapper)
        {
            _appEFContext = appEFContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await _appEFContext.Categories
                .Where(c => !c.IsDeleted)
                .Select(x => _mapper.Map<CategoryItemViewModel>(x))
                .ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateViewModel model)
        {
            var cat = _mapper.Map<CategoryEntity>(model);
            await _appEFContext.Categories.AddAsync(cat);
            await _appEFContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = _appEFContext.Categories
                .Where(c => !c.IsDeleted)
                .SingleOrDefault(x => x.Id == id);
            if (cat == null)
            {
                return NotFound();
            }
            cat.IsDeleted = true;
            await _appEFContext.SaveChangesAsync();
            return Ok();
        }


    }
}
