using LACE.Core.Messages;
using LACE.Core.Models;
using LACE.Core.Repository;
using LACE.Core.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nedesk.Core.DataBase.Factory;
using Nedesk.Core.Interfaces;
using Nedesk.Core.Models;
using Nedesk.Core.Security.Models;
using Nedesk.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LACE.Core.Auth
{
    public class AuthService : NDIAuthenticationService<AuthUser, TokenPayload>
    {
        private readonly AuthUserRepository _userRepository;
        private readonly NDITokenService<TokenPayload> _tokenService;
        private readonly NDIDBConnectionFactory _connectionFactory;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger _logger;

        public AuthService(AuthUserRepository userRepository,
                           NDITokenService<TokenPayload> tokenService,
                           NDIDBConnectionFactory connectionFactory,
                           IHttpContextAccessor httpContextAccessor,
                           ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _connectionFactory = connectionFactory;
            _contextAccessor = httpContextAccessor;
            _logger = logger;
        }


        public NDResponse<AuthUser> GetTokenUser(NDToken<TokenPayload> token) 
        {
            NDResponse<AuthUser> response = new NDResponse<AuthUser>();
            TokenPayload? payload = JsonSerializer.Deserialize<TokenPayload>(token.Payload);
            if (payload == null)
            {
                response.AddValidationMessage(MessageCodesList.Get("LCEAUTH001"));
                return response;
            }

            using (DbConnection connection = _connectionFactory.GetReadOnlyConnection())
            {
                NDListRequest request = new NDListRequest();
                request.Filters.Add(new NDFilter
                {
                    Target1 = "Email",
                    OperationType = Nedesk.Core.Enums.NDFilterOperationTypeEnum.Equals,
                    Value1 = payload.Login

                });

                NDListResponse<AuthUser> authUser = _userRepository.FindByRequest(connection, request);

                if (authUser.HasAnyMessages || !authUser.ResponseData.Any())
                {
                    response.AddValidationMessage(MessageCodesList.Get("LCEAUTH001"));
                    return response;
                }

                response.ResponseData = authUser.ResponseData.First();
            }

            return response;
        }
       
    }
}
