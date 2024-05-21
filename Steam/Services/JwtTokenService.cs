using Microsoft.IdentityModel.Tokens;
using Steam.Data.Entities.Identity;
using Steam.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Steam.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim("email", user.Email),
                new Claim("name", user.NickName)
            };
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSecretKey"));

            var signinKey = new SymmetricSecurityKey(key);

            var signinCredential = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredential,
                expires: DateTime.Now.AddDays(10),
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
