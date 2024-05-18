using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities
{
    [Table("tblGameCategory")]
    public class GameCategoryEntity
    {
        [ForeignKey("Game")]
        public int GameId { get; set; }
        public virtual GameEntity Game { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
    }
}
