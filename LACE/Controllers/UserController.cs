using LACE.Core.Business;
using LACE.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nedesk.Core.API;
using Nedesk.Core.Interfaces;

namespace LACE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : NDBaseController
    {
        private AuthUserBusiness _authUserBusiness;
        public UserController(AuthUserBusiness authUserBusiness ,ILogger<NDBaseController> logger, IAuth AuthService, IHttpContextAccessor httpContextAccessor) : base(logger, AuthService, httpContextAccessor)
        {
            _authUserBusiness = authUserBusiness; 
        }

        [HttpPost]
        public ActionResult<IResponse<long>> Create(AuthUser user)
        {
            return GetResponse(() => _authUserBusiness.Insert(user));
        }

        [HttpPut]
        public ActionResult<IResponse<bool>> Update(AuthUser user)
        {
            return GetResponse(() => _authUserBusiness.Update(user));
        }
    }
}
