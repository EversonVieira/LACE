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
using System.Text.Json;

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
                               NDIAuthenticationService<AuthUser, TokenPayload> authenticationService,
                               ILogger<LoginController> logger) : base(logger, tokenFactory, authenticationService, httpContextAccessor)
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
                    Target1 = nameof(AuthUser.Password),
                    Value1 = authUser.Password,
                },
            });

            var userResponse = _authUserBusiness.FindByRequest(request);

            if (userResponse.HasAnyMessages)
            {
                return Ok(userResponse);
            }

            if (!userResponse.ResponseData.Any())
            {
                userResponse.AddValidationMessage(MessageCodesList.Get("LCEAUTH001"));
                return Ok(userResponse);

            }

            NDResponse<string> tokenResponse = new NDResponse<string>();
            tokenResponse.ResponseData = this._tokenFactory.CreateToken(new TokenPayload
            {
                Login = authUser.Email,
                RefreshData = DateTime.UtcNow,
                RefreshToken = true,
            }).AsString;

            return Ok(tokenResponse);
        }

        [HttpPatch("refresh")]
        [NDAuthenticate]
        public ActionResult<NDResponse<NDToken<TokenPayload>>> RefreshToken()
        {
            var token = this._tokenFactory.CreateToken(_currentUserToken);
            var payload = JsonSerializer.Deserialize<TokenPayload>(token.Payload);

            if (payload == null)
            {
                return Unauthorized();
            }

            payload.RefreshData = DateTime.UtcNow;
            return Ok(this._tokenFactory.CreateToken(payload).AsString);
        }

        [HttpGet("user")]
        [NDAuthenticate]
        public ActionResult<NDResponse<AuthUser>> GetSessionUser()
        {
            NDResponse<AuthUser> userResponse = new NDResponse<AuthUser>();
            userResponse.ResponseData = this.RetrieveCurrentUser();
            return Ok(userResponse);
        }

    }
}
