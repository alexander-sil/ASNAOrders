using ASNAOrders.Web.Administration.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;


namespace ASNAOrders.Web.Administration.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public virtual IActionResult Authenticate([FromBody] AuthenticationRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                {
                    return BadRequest("Username and/or password not specified.");
                }

                UserAccount account;

                try
                {
                    account = _accountRepository.Search(model.Username)[0];
                }
                catch (IndexOutOfRangeException)
                {
                    return BadRequest("This user does not exist.");
                }

                string hash = Convert.ToHexString(SHA256.Create().ComputeHash(Encoding.Latin1.GetBytes($"{model.Password}~{account.PasswordSalt}~{AccountRepository.SecretKey}")));

                if (hash == account.PasswordHash)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.Latin1.GetBytes(""));
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                             new Claim("username", account.Username),
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(15),
                        Issuer = "http://localhost:5500/",
                        Audience = "http://localhost:5500/",
                        SigningCredentials = new SigningCredentials
                        (secretKey,
                        SecurityAlgorithms.HmacSha512Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);
                    var stringToken = tokenHandler.WriteToken(token);
                    return Ok(stringToken);
                }
                return Unauthorized("Unauthorized");

            }
            catch
            {
                return BadRequest
                ("An error occurred in generating the token.");
            }
        }

    }
}
