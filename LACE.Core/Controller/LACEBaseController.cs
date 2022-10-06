using LACE.Core.Auth;
using LACE.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nedesk.Core.API;
using Nedesk.Core.Interfaces;
using Nedesk.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Controller
{
    public class LACEBaseController : NDBaseController<AuthUser, TokenPayload>
    {
        public LACEBaseController(ILogger<NDBaseController<AuthUser, TokenPayload>> logger,
                                  NDITokenFactory<TokenPayload> tokenFactory,
                                  NDIAuthenticationService<AuthUser, TokenPayload> authenticationService,
                                  IHttpContextAccessor httpContextAccessor) : base(logger, tokenFactory, authenticationService, httpContextAccessor)
        {

        }
    }
}
