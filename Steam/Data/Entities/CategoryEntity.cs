using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace Steam.Data.Entities
{
    [Table("tblCategory")]
    public class CategoryEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }

        public virtual ICollection<GameCategoryEntity> GameCategories { get; set; } 
    }
}
