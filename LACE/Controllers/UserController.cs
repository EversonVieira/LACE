using LACE.Core.Adapter;
using LACE.Core.Business;
using LACE.Core.Models;
using LACE.Core.Models.DTO;
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
        private AuthUserAdapter _authUserAdapter;
        public UserController(AuthUserAdapter authUserAdapter ,ILogger<NDBaseController> logger, IAuth AuthService, IHttpContextAccessor httpContextAccessor) : base(logger, AuthService, httpContextAccessor)
        {
            _authUserAdapter = authUserAdapter; 
        }

        [HttpPost]
        public ActionResult<IResponse<long>> Create(DTO_AuthUser_Register user)
        {
            return GetResponse(() => _authUserAdapter.Insert(user), requireAuthentication: false);
        }

        [HttpPut]
        public ActionResult<IResponse<bool>> Update(DTO_AuthUser_Update user)
        {
            return GetResponse(() => _authUserAdapter.Update(user));
        }
    }
}
