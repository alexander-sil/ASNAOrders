using ASNAOrders.Web.Administration.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ASNAOrders.Web.Administration.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public virtual IActionResult Authenticate([FromBody] AuthenticationRequest request)
        {

        }

    }
}
