using LACE.Core.Adapter;
using LACE.Core.Auth;
using LACE.Core.Business;
using LACE.Core.Controller;
using LACE.Core.Models;
using LACE.Core.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Nedesk.Core.Attributes;
using Nedesk.Core.Interfaces;
using Nedesk.Core.Models;
using Nedesk.Core.Security.Factory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LACE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [NDAuthenticate]

    public class UserController : LACEBaseController
    {
        private readonly AuthUserAdapter _authUserAdapter;

        public UserController(AuthUserAdapter authUserAdapter,
                              ILogger<UserController> logger,
                              NDITokenFactory<TokenPayload> tokenFactory,
                              IHttpContextAccessor httpContextAccessor,
                              NDIAuthenticationService<AuthUser, TokenPayload> authenticationService,
                              ILogger<LoginController> UserController) : base(logger, tokenFactory, authenticationService, httpContextAccessor)
        {
            this._authUserAdapter = authUserAdapter;
        }

        [HttpPost]
        public ActionResult<NDResponse<int>> Create(DTO_AuthUser_Register user)
        {
            return Ok(_authUserAdapter.Insert(user));
        }

        [HttpPut]
        public ActionResult<NDResponse<int>> Update(DTO_AuthUser_Update user)
        {
            return Ok(_authUserAdapter.Update(user));
        }

    }
}
