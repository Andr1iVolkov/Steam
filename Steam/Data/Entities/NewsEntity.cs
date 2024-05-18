using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities
{
    [Table("tblNews")]
    public class NewsEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Title { get; set; }
        [Required, StringLength(4000)]
        public string Description { get; set; }
        [Required]
        DateTime DateOfRelease { get; set; }
        [Required, StringLength(500)]
        public string Image { get; set; }
        [Required, StringLength(500)]
        public string VideoURL { get; set; }
        [Required]
        public int GameId { get; set; }
        public virtual GameEntity Game { get; set; }
    }
}
