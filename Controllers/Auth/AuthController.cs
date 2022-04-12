using Microsoft.AspNetCore.Mvc;
using MainLABAPI.Data;
using MainLABAPI.Data.Models.DB;
using MainLABAPI.Data.Models.DTO;
using MainLABAPI.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace MainLABAPI.Controllers.Auth
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ApplicationContext _context;
        private readonly IAuthService _authService;

        public AuthController(ApplicationContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterPost(RegistrationDataModel model)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest();
            }

            try
            {
                var user = _authService.UserVerification(model.Username, model.Password);
                if (user != null)
                {
                    StatusCode(400, "Something went wrong in method _MethodName.");
                }
                await _authService.AddUser(model);
                return await LoginPost(new AuthDataModel
                {
                    Username = model.Username,
                    Password = model.Password,
                });
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong in method _MethodName.");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginPost(AuthDataModel model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(500, "Something went wrong in method _MethodName.");
            }

            try
            {
                var identity = _authService.GetIdentity(model.Username, model.Password);
                if (identity == null)
                {
                    return BadRequest(new { errorText = "Invalid username or password." });
                }

                var now = DateTime.UtcNow;
                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                    issuer: JwtConfigurations.Issuer,
                    audience: JwtConfigurations.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(new TimeSpan(JwtConfigurations.Lifetime/60,0,0)),
                    signingCredentials: new SigningCredentials(JwtConfigurations.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    token = encodedJwt,
                };


                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong in method _MethodName.");
            }
        }

        [HttpPost]
        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> LogoutPost([FromHeader] string Authorization = "Barrier{token}")
        {
            var token = Authorization.Split(" ")[1];
            var decodeJwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            
            return Ok(decodeJwt);
        }
    }
}
