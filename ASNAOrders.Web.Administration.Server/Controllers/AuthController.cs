<<<<<<< HEAD
﻿using ASNAOrders.Web.Administration.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
=======
﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
>>>>>>> 6b5f351633e2f9cdffbd48e0d2611a745182f6a9

namespace ASNAOrders.Web.Administration.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
<<<<<<< HEAD
        public virtual IActionResult Authenticate([FromBody] AuthenticationRequest request)
        {

        }
=======
        [SwaggerResponse(500)]
        public virtual IActionResult Authenticate([FromBody] )
>>>>>>> 6b5f351633e2f9cdffbd48e0d2611a745182f6a9
    }
}
