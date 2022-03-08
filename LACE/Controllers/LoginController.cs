using LACE.Core.Models;
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
        public ActionResult<IResponse<Nedesk.Core.Interfaces.ISession>> Login(AuthUser user)
        {
            return GetResponse(() => _authService.CreateSession(user), requireAuthentication: false);
        }

        [HttpGet]
        public ActionResult<IResponse<bool>> ValidateSession()
        {
            return GetResponse(() => _authService.ValidateSession(), requireAuthentication: false);
        }

        [HttpDelete]
        public ActionResult<IResponse<bool>> DropSession()
        {
            return GetResponse(() => _authService.DropSession(), requireAuthentication: false);
        }


    }
}
