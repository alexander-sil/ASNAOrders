using ASNAOrders.Web.Administration.Server.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Security.Cryptography;
using System.Text;
using ASNAOrders.Web.Administration.Server.LogicServices;
using ASNAOrders.Web.Administration.Server.AbstrModels;

namespace ASNAOrders.Web.Administration.Server.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private CustomDbContext Context { get; set; }

        public AdministrationController(CustomDbContext context)
        {
            Context = context;
        }

        [HttpDelete]
        [Route("ban-user")]
        [Authorize(Policy = "Operator")]
        public IActionResult BanUser([FromHeader] string username, [FromHeader] string reason)
        {
            Context.Users.First(f => f.Username == username).BanIssued = true;
            Context.Users.First(f => f.Username == username).BanReason = reason;

            Context.SaveChanges();

            return Ok();
        }

        [HttpPatch]
        [Route("unban-user")]
        [Authorize(Policy = "Operator")]
        public IActionResult UnbanUser([FromHeader] string username)
        {
            Context.Users.First(f => f.Username == username).BanIssued = false;
            Context.Users.First(f => f.Username == username).BanReason = string.Empty;

            Context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("create-user")]
        [Authorize(Policy = "Operator")]
        public IActionResult CreateUser([FromHeader] string username, [FromHeader] string password, [FromHeader] string permissions)
        {

            if (Context.Users.Where(f => f.Username == username).Count() == 0)
            {
                byte[] salt = new byte[32];

                Context.Users.Add(new DataAccess.EntityDataModels.UserEntityDataModel()
                {

                    Username = username,
                    PasswordHash = Convert.ToHexString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes($"{username}~{password}~{salt}"))),
                    EncryptedPasswordSalt = Convert.ToHexString(ProtectedData.Protect(salt, null, DataProtectionScope.LocalMachine)),
                    Permissions = new DataAccess.EntityDataModels.UserPermissionsDataModel()
                    {
                        Operator = permissions.Contains("O") ? true : false,
                        OptionsViewEdit = permissions.Contains("W") ? true : false,
                        OptionsView = permissions.Contains("R") ? true : false
                    },
                    BanIssued = false

                });

                Context.SaveChanges();
            }

            return Ok();
        }

        [HttpPut]
        [Route("change-password")]
        [Authorize(Policy = "Operator")]
        public IActionResult ChangePassword([FromHeader] string username, [FromHeader] string password)
        {
            if (Context.Users.Where(f => f.Username == username).Count() > 0)
            {
                var user = Context.Users.First(f => f.Username == username);
                byte[] salt = new byte[32];

                user.PasswordHash = Convert.ToHexString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes($"{username}~{password}~{salt}")));
                user.EncryptedPasswordSalt = Convert.ToHexString(ProtectedData.Protect(salt, null, DataProtectionScope.LocalMachine));

                Context.SaveChanges();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("delete-user")]
        [Authorize(Policy = "Operator")]
        public IActionResult DeleteUser([FromHeader] string username)
        {

            if (Context.Users.Where(f => f.Username == username).Count() > 0)
            {
                var user = Context.Users.First(f => f.Username == username);

                Context.Remove(user);
                Context.SaveChanges();
            }

            return Ok();
        }

        [HttpPut]
        [Route("change-permissions")]
        [Authorize(Policy = "Operator")]
        public IActionResult DeleteUser([FromHeader] string username, [FromHeader] string permissions)
        {
            if (Context.Users.Where(f => f.Username == username).Count() > 0)
            {
                var user = Context.Users.First(f => f.Username == username);

                user.Permissions = new DataAccess.EntityDataModels.UserPermissionsDataModel()
                {
                    Operator = permissions.Contains("O") ? true : false,
                    OptionsViewEdit = permissions.Contains("W") ? true : false,
                    OptionsView = permissions.Contains("R") ? true : false
                };

                Context.SaveChanges();
            }
            
            return Ok();
        }

        [HttpGet]
        [Route("get-users")]
        [Authorize(Policy = "Read")]
        public IActionResult GetUsers()
        {
            return Ok(Context.Users.ToList());
        }

        [HttpGet]
        [Route("get-configuration")]
        [Authorize(Policy = "Read")]
        public IActionResult GetConfiguration([FromHeader] string server, [FromHeader] string port, [FromHeader] string vhost)
        {
            return Ok(RabbitMQService.GetConfig(server, port, vhost));
        }

        [HttpPost]
        [Route("issue-configuration")]
        [Authorize(Policy = "ReadEdit")]
        public IActionResult IssueConfigurationOptions([FromBody] IssuibleConfig config, [FromHeader] string server, [FromHeader] string port, [FromHeader] string vhost)
        {
            RabbitMQService.IssueConfigurationOptions(config, server, ushort.Parse(port), vhost);
            return Ok();
        } 
    }
}
