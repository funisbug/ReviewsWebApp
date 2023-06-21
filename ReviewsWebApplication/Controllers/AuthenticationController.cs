using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Reviews.Domain.Models;
using Reviews.Domain.Services;
using ReviewsWebApplication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ConfigurationManager = Reviews.Domain.Services.ConfigurationManager;

namespace ReviewsWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<ReviewController> logger;
        private readonly ILoginService loginService;
        private readonly IMapper mapper;

        public AuthenticationController(ILogger<ReviewController> logger, ILoginService loginService, IMapper mapper)
        {
            this.logger = logger;
            this.loginService = loginService;
            this.mapper = mapper;
        }
        
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginApi user)
        {  
            try
            {
                var res = loginService.CheckLogin(mapper.Map<Login>(user));
                if (res)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"], audience: ConfigurationManager.AppSetting["JWT:ValidAudience"], claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(6), signingCredentials: signinCredentials);
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new JWTTokenResponse
                    {
                        Token = tokenString
                    });
                }
                return Unauthorized();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
                return BadRequest(new { Error = e.Message });
            }
        }
    }
}
