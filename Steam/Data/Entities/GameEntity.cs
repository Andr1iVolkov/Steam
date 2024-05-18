using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities
{
    [Table("tblGame")]
    public class GameEntity : BaseEntity<int>
    {
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
        public virtual ICollection<GameCategoryEntity> GameCategories { get; set; }
        public virtual ICollection<GameImageEntity> GameImages { get; set; }
    }
}
