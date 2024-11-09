using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Clean.Architecture.Inventory.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public AuthController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{

			if (model.Username == "testuser" && model.Password == "password") // Exemplo fixo de validação
			{
				var claims = new[]
				{
				new Claim(ClaimTypes.Name, model.Username)

			};

				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
				var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

				var expiration = DateTime.UtcNow.AddMinutes(model.TokenLifetime);

				var token = new JwtSecurityToken(
					claims: claims,
					expires: expiration,
					signingCredentials: creds);

				return Ok(new
				{
					Token = new JwtSecurityTokenHandler().WriteToken(token)
				});
			}

			return Unauthorized();
		}
	}

	public class LoginModel
	{
		public string Username { get; set; } = "testuser";
		public string Password { get; set; } = "password";
		public int TokenLifetime { get; set; } = 1;
	}
}