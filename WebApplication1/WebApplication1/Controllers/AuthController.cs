using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("Login")]
        public IActionResult Login()
        {
            string returnUrl = "http://google.com/";

            return Challenge(new AuthenticationProperties
            {
                RedirectUri = returnUrl,
            }, OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
