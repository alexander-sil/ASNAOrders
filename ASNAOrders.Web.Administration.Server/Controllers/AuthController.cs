using ASNAOrders.Web.Administration.Server.DataAccess;
using ASNAOrders.Web.Administration.Server.DataAccess.EntityDataModels;
using ASNAOrders.Web.Administration.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ASNAOrders.Web.Administration.Server.LogicServices;


namespace ASNAOrders.Web.Administration.Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private CustomDbContext Context { get; set; }

        public AuthController(CustomDbContext context)
        {
            Context = context;

            if (Context.Users.FirstOrDefault(f => f.Username == Properties.Resources.AdminString) == null)
            {
                RandomNumberGenerator generator = new RNGCryptoServiceProvider();
                byte[] salt = new byte[32];

                generator.GetBytes(salt);

                Context.Users.Add(new DataAccess.EntityDataModels.UserEntityDataModel()
                {
                    Username = Properties.Resources.AdminString,
                    PasswordHash = Convert.ToHexString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes($"{Properties.Resources.AdminString}~{Properties.Resources.AdminString}~{salt}"))),
                    EncryptedPasswordSalt = Convert.ToHexString(ProtectedData.Protect(salt, null, DataProtectionScope.LocalMachine)),
                    Permissions = new UserPermissionsDataModel()
                    {
                        Operator = true,
                        OptionsViewEdit = true,
                        OptionsView = true,
                    },
                    BanIssued = false,
                    BanReason = null
                });

                Context.SaveChanges();
            }
        }

        [HttpPost]
        [Route("authenticate")]
        public virtual IActionResult Authenticate([FromBody] AuthenticationRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest(new AuthenticationResponse()
                    {
                        Result = false,
                        AccessToken = null,
                        Information = new AuthenticationResponseErrorsInner()
                        {
                            Code = Guid.NewGuid().ToString(),
                            Message = Properties.Resources.UsernameOrPasswordNotSpecifiedString
                        }
                    });
                }

                UserEntityDataModel user;

                try
                {
                    user = Context.Users.Where(f => f.Username == request.Username).ToArray()[0];
                }
                catch (IndexOutOfRangeException)
                {
                    return BadRequest(new AuthenticationResponse() {
                        Result = false,
                        AccessToken = null,
                        Information = new AuthenticationResponseErrorsInner()
                        {
                            Code = Guid.NewGuid().ToString(),
                            Message = Properties.Resources.UserInexistentString
                        }
                    });
                }

                string hash = Convert.ToHexString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes($"{request.Username}~{request.Password}~{ProtectedData.Unprotect(Convert.FromHexString(user.EncryptedPasswordSalt), null, DataProtectionScope.LocalMachine)}")));

                if (hash == user.PasswordHash)
                {
                    if (user.BanIssued)
                    {
                        return Unauthorized(Properties.Resources.BannedString + user.BanReason != null ? user.BanReason : "Generic");
                    }

                    var secretKey = new SymmetricSecurityKey(SecretGeneratorService.GetIssuerSigningKey(Properties.Resources.TokenSigningKeyFilePathString));
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                             new Claim(type: "username", value: user.Username),
                             new Claim(type: "permissions", value: user.Permissions.Operator ? "RWO" : user.Permissions.OptionsViewEdit ? "RW" : "R")
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(15),
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
                return Unauthorized(Properties.Resources.UnauthString);

            }
            catch
            {
                return StatusCode(500, Properties.Resources.TokenGenerationErrorString);
            }
        }

    }
}
