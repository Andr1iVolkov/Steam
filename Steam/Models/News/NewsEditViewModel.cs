using System.ComponentModel.DataAnnotations;

namespace Steam.Models.News
{
    public class NewsEditViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(4000)]
        public string Description { get; set; }

        [Required]
        public DateTime DateOfRelease { get; set; }

        [Required]
        [StringLength(500)]
        public string Image { get; set; }

        [Required]
        [StringLength(500)]
        public string VideoURL { get; set; }

        [Required]
        public int GameId { get; set; }
    }
}
