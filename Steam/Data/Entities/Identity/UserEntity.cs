using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steam.Data.Entities.Identity
{
    [Table("tblUser")]
    public class UserEntity : IdentityUser<int>
    {
        [Required, StringLength(100)]
        public string NickName { get; set; }
        [Required, StringLength(100)]
        public string Password { get; set; }
       
        [StringLength(2500)]
        public string Photo { get; set; }
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
        public virtual ICollection<UserGameEntity> UserGames { get; set; }
    }
}
