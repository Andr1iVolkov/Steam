using System.ComponentModel.DataAnnotations;

namespace Steam.Models.Game
{
    public class GameEditViewModel
    {
        public int Id { get; set; } 

        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required, StringLength(1000)]
        public string Description { get; set; }
        [Required]
        public DateTime DateOfRelease { get; set; }
        [Required, StringLength(1000)]
        public string SystemRequirements { get; set; }

        public List<int> SelectedCategoryIds { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
