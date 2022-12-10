using E_HealthCareApplication.Models;
using E_HealthCareApplication.NewFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_HealthCareApplication.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class LoginUserController : ControllerBase
    {
        private readonly HealthCareDbContext _context;
        private readonly IConfiguration _configuration;

        public LoginUserController(HealthCareDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(Account request)
        {
            var user_exist = await _context.Accounts.FindAsync(request.Email);

            if (user_exist != null)
                return BadRequest("User already exists.");

            //create a user
            var account = new Account
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                DateOfBirth = request.DateOfBirth,
                Phone = request.Phone,
                Address = request.Address,
                IsAdmin = request.IsAdmin
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            var jwtToken = GenerateJWTToken(account);

            return Ok(jwtToken);
        }

        [HttpPost("loginUser")]
        public async Task<ActionResult<string>> LoginUser(UserRequest user)
        {
            var existingUser = await _context.Accounts.Where(s => s.Email == user.Email).FirstOrDefaultAsync();

            if (existingUser is null)
                return BadRequest("User doesn't exist");

            if (existingUser.Email != user.Email && existingUser.Password != user.Password)
                return BadRequest("Invalid Credentials");

            var jwtToken = GenerateJWTToken(existingUser);

            return Ok(jwtToken);
        }

        private string GenerateJWTToken(Account userAccount)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection(key: "JwtConfig:Secret").Value);

            //TokenDescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(type: "Id", value: userAccount.Id.ToString()),
                    new Claim(ClaimTypes.Email, userAccount.Email),
                    new Claim(ClaimTypes.Role, userAccount.IsAdmin == 1 ? "Admin" : "User")
                }),

                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), algorithm: SecurityAlgorithms.HmacSha256)

            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
