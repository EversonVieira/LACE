using Microsoft.AspNetCore.Mvc;
using Nedesk.Core.API;
using Nedesk.Core.Interfaces;
using Nedesk.Core.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LACE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController:NDBaseController
    {
        public LoginController(ILogger<LoginController> logger, IAuth auth, IHttpContextAccessor httpContextAccessor)
            : base(logger, auth, httpContextAccessor){}


        [HttpPost]
        public ActionResult<object> Login()
        {
            return GetResponse(() => _authService.CreateSession());
        }


        
    }
}
