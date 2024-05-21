using Steam.Data.Entities.Identity;

namespace Steam.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> CreateTokenAsync(UserEntity user);
    }
}
