using LACE.Core.Auth;
using LACE.Core.Business;
using LACE.Core.Controller;
using LACE.Core.Helper;
using LACE.Core.Messages;
using LACE.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Nedesk.Core.Attributes;
using Nedesk.Core.Interfaces;
using Nedesk.Core.Models;
using Nedesk.Core.Security.Factory;
using Nedesk.Core.Security.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LACE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : LACEBaseController
    {
        private readonly AuthUserBusiness _authUserBusiness;
        private readonly LoginHelper _loginHelper;

        public LoginController(AuthUserBusiness authUserBusiness,
                               LoginHelper loginHelper,
                               NDITokenFactory<TokenPayload> tokenFactory,
                               IHttpContextAccessor httpContextAccessor,
                               ILogger<LoginController> logger) : base(logger, tokenFactory, httpContextAccessor)
        {
            this._authUserBusiness = authUserBusiness;
            this._loginHelper = loginHelper;
        }

        [HttpPost]
        public ActionResult<NDResponse<NDToken<TokenPayload>>> Login(AuthUser authUser)
        {
            _loginHelper.EncrpytUserSensistiveInformation(authUser);

            NDListRequest request = new NDListRequest();
            request.Filters.AddRange(new List<NDFilter>()
            {
                new NDFilter
                {
                    Target1 = nameof(AuthUser.Email),
                    Value1 = authUser.Email,
                },
                new NDFilter
                {
                    Target1 = nameof(AuthUser.Email),
                    Value1 = authUser.Password,
                },
            });

            var userResponse = _authUserBusiness.FindByRequest(request);

            if (userResponse.HasAnyMessages)
            {
                return Unauthorized();
            }

            if (!userResponse.ResponseData.Any())
            {
                userResponse.AddValidationMessage(MessageCodesList.Get("LCEAUTH001"));
            }

            return Ok(this._tokenFactory.CreateToken(new TokenPayload
            {
                Login = authUser.Email,
                RefreshData = DateTime.UtcNow,
                RefreshToken = true,
            }));
        }

        [HttpGet]
        [NDAuthenticate]
        public ActionResult<NDResponse<AuthUser>> GetSessionUser()
        {
            AuthUser item = this.RetrieveCurrentUser();
            item.Password = String.Empty;
            return Ok(item);
        }

    }
}
