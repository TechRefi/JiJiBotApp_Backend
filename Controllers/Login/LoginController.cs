using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JiJiBotApp_Backend.Controllers.BaseApi;
using JiJiBotApp_Backend.DTOs.Model.Login;
using JiJiBotApp_Backend.DTOs.Model.Response;
using JiJiBotApp_Backend.DTOs.SearchRequests.Login;
using JiJiBotApp_Backend.Services.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JiJiBotApp_Backend.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(ILoginService userService, IConfiguration configuration, ILogger<LoginController> logger) : BaseApiController
    {
        private readonly ILoginService _userService = userService;
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<LoginController> _logger = logger;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginModel>>> Login([FromBody] LoginAuthRequestModel request)
        {
            try
            {
                // Validate user credentials
                var user = await _userService.ValidateUserAsync(request);
                if (user == null)
                {
                    return HandleSuccess(new LoginModel(), "Invalid username or password");
                }

                // Generate JWT token
                var token = GenerateJwtToken(user);

                var response = new LoginModel
                {
                   
                    Token = token,
                    User = user
                };

                return HandleSuccess(response, "Login successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return HandleError<LoginModel>(ex, "Failed to authenticate");
            }
        }

        private string GenerateJwtToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
               
                new("UserId", user.UserId.ToString()),
                new("EnteredDateTime", user.EnteredDateTime.ToString("dd/MMM/yyyy")),
                new("UserName", user.UserName),
                new("UserPassword", user.UserPassword),
                new("RoleId", user.RoleId.ToString()),
                new("UserEmail", user.UserEmail),
               
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3), // Token expires after 3 hours
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
