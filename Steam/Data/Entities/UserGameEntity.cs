using Steam.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities
{
    [Table("tblUserGame")]
    public class UserGameEntity
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; }

        public int GameId { get; set; }
        public GameEntity Game { get; set; }
    }
}
