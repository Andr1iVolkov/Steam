using Steam.Models.Category;
using Steam.Models.News;

namespace Steam.Models.Game
{
    public class GameItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime DateOfRelease { get; set; }

        public string SystemRequirements { get; set; }

        public List<CategoryItemViewModel> Categories { get; set; }
        public List<GameImageViewModel> Images { get; set; }
        public List<NewsItemViewModel> News { get; set; }
    }
}
