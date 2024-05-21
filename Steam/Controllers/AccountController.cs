using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Steam.Constants;
using Steam.Data;
using Steam.Data.Entities.Identity;
using Steam.Helpers;
using Steam.Interfaces;
using Steam.Models.Account;

namespace Steam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private IJwtTokenService _jwtTokenService;
        private readonly AppEFContext _context; 

        public AccountController(UserManager<UserEntity> userManager,
            IJwtTokenService jwtTokenService,
            AppEFContext context)
        {
            _context = context;
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var users = _context.Users.ToList();
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    return BadRequest("Користувача з таким email не існує");
                }

                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    return BadRequest("Неправильний пароль");
                }

                var token = await _jwtTokenService.CreateTokenAsync(user);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            try
            {
                string imageName = string.Empty;
                if (!string.IsNullOrEmpty(model.ImageBase64))
                {
                    imageName = await ImageWorker.SaveImageAsync(model.ImageBase64);
                }
                var user = new UserEntity
                {
                    NickName = model.NickName,
                    Password = model.Password,
                    Email = model.Email,
                    UserName = model.Email,
                    Photo = imageName,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(user, Roles.User);
                }
                else
                    return BadRequest("Щось пішло не так!");

                var token = await _jwtTokenService.CreateTokenAsync(user);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
