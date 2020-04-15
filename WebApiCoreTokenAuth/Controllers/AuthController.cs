using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebApiCoreTokenAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("token")]
        public ActionResult GetToken()
        {
            //security key.. this should not be hard coded
            string securityKey = "long_security_key_generated_for_specific_environment_$@#2020";
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                    issuer: "$@#2020",
                    audience: "viewers",
                    expires: DateTime.Now.AddDays(5),
                    signingCredentials: signingCredentials
                );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}