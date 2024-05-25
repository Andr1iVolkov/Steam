using AutoMapper;
using Steam.Data;
using Steam.Data.Entities;
using Steam.Models.Category;
using Steam.Models.Helpers;

namespace Steam.Mapper
{
    public class AppMapProfile : Profile
    {
        private readonly AppEFContext _context;
        public AppMapProfile(AppEFContext context) 
        {
            _context = context;

            CreateMap<CategoryEntity, CategoryItemViewModel>();
            CreateMap<CategoryCreateViewModel, CategoryEntity>();

        }
    }
}
