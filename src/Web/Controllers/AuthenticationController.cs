using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IConfiguration config, IAuthenticationService authenticationService)
        {
            _config = config;
            _authenticationService = authenticationService;
        }

        [HttpPost("[action]")]
        public IActionResult AdminAuthenticate([FromBody] CredentialsRequest credentials)
        {
            string token = _authenticationService.Authentication(credentials);
            return Ok(token);
        }

        [HttpPost("[action]")]
        public IActionResult ClientAuthenticate([FromBody] CredentialsRequest credentials)
        {
            string token =  _authenticationService.Authentication(credentials);
            return Ok(token);
        }
    }
}
