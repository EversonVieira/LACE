using LACE.Core.Models;
using LACE.Core.Repository;
using LACE.Core.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nedesk.Core.Interfaces;
using Nedesk.Core.Models;
using Nedesk.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Auth
{
    public class AuthService : IAuth
    {
        private readonly AuthSessionRepository _sessionRepository;
        private readonly AuthUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthService(AuthSessionRepository sessionRepository,
                           AuthUserRepository userRepository,
                           IHttpContextAccessor httpContextAccessor,
                           ILogger<AuthSessionRepository> logger)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _contextAccessor = httpContextAccessor;
            _logger = logger;
        }


        public Response<Nedesk.Core.Interfaces.ISession> CreateSession(IAuthUser user)
        {
            Response<Nedesk.Core.Interfaces.ISession> response = new Response<Nedesk.Core.Interfaces.ISession>();

            BaseListRequest request = new BaseListRequest();
            request.Filters.AddRange(new List<Filter>()
            {
                new Filter
                {
                    Target1 = nameof(AuthUser.Email),
                    OperationType = FilterOperationType.Equals,
                    Value1 = user.Email,
                    AggregateType = FilterAggregateType.AND,

                },
                new Filter
                {
                    Target1 = nameof(AuthUser.Password),
                    OperationType = FilterOperationType.Equals,
                    Value1 = LoginUtility.EncryptPassword(user.Password),
                    AggregateType = FilterAggregateType.AND,
                },
            });

            ListResponse<AuthUser> users = _userRepository.FindByRequest(request);
            if (users.InError)
            {
                response.Merge(users);
                return response;
            }

            if (!users.ResponseData.Any())
            {
                response.AddValidationMessage("911", "Nenhum usuário encontrado!");
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }

            AuthUser searchUser = users.ResponseData.First();

            string sessionKey = LoginUtility.GenerateSession(searchUser);

            AuthSession session = new AuthSession()
            {
                UserId = searchUser.Id,
                SessionKey = sessionKey,
                LastRenewDate = DateTime.Now
            };

            var sessionResponse = _sessionRepository.Insert(session);

            if (sessionResponse.InError)
            {
                response.Merge(sessionResponse);
                return response;
            }

            session.Id = sessionResponse.ResponseData;
            response.ResponseData = session;

            response.AddInformationMessage("911", "Logado com sucesso!");

            _userRepository.CloseConnection();
            return response;
        }

        public Response<bool> DropSession()
        {
            Response<bool> response = new Response<bool>();
            string session = _contextAccessor.HttpContext.Request.Headers["Session"];
            if (session.IsNullOrEmpty())
            {
                _logger.LogWarning("Session not provided and the code isn't supposed to reach here without a stored session");
                return response;
            }

            BaseListRequest sessionRequest = new BaseListRequest();
            sessionRequest.Filters.Add(new Filter()
            {
                Target1 = "SessionKey",
                OperationType = FilterOperationType.Equals,
                Value1 = session
            });


            var sessionResponse = _sessionRepository.FindByRequest(sessionRequest);
            if (sessionResponse.InError || !sessionResponse.ResponseData.Any())
            {
                response.Merge(sessionResponse);
                return response;
            }

            _sessionRepository.CloseConnection();
            var removeResponse = _sessionRepository.DeleteById(sessionResponse.ResponseData.First().Id);
            return removeResponse;
        }

        public Response<IAuthUser> GetSessionUser()
        {
            Response<IAuthUser> response = new Response<IAuthUser>();

            string session = _contextAccessor.HttpContext.Request.Headers["Session"];
            if (session.IsNullOrEmpty())
            {
                _logger.LogWarning("Session not provided and the code isn't supposed to reach here without a stored session");
                return response;
            }

            BaseListRequest sessionRequest = new BaseListRequest();
            sessionRequest.Filters.Add(new Filter()
            {
                Target1 = "SessionKey",
                OperationType = FilterOperationType.Equals,
                Value1 = session
            });


            var sessionResponse = _sessionRepository.FindByRequest(sessionRequest);
            if (sessionResponse.InError)
            {
                response.Merge(sessionResponse);
                return response;
            }

            if (!sessionResponse.ResponseData.Any())
            {
                response.AddValidationMessage("911", "Usuário não encontrado");
                return response;
            }

            BaseListRequest request = new BaseListRequest();
            request.Filters.Add(new Filter
            {
                Target1 = "Id",
                OperationType = FilterOperationType.Equals,
                Value1 = sessionResponse.ResponseData.First().UserId
            });

            var listResponse = _userRepository.FindByRequest(request);
            if (listResponse.HasAnyMessages)
            {
                response.Merge(listResponse);
                return response;
            }

            if (!listResponse.ResponseData.Any())
            {
                response.AddValidationMessage("911", "Usuário não encontrado");
                return response;
            }

            response.ResponseData = listResponse.ResponseData.First();
            response.StatusCode = HttpStatusCode.OK;
            _sessionRepository.CloseConnection();
            return response;
        }

        public Response<bool> ValidateSession()
        {
            Response<bool> response = new Response<bool>();
            string session = _contextAccessor.HttpContext.Request.Headers["Session"];
            if (session.IsNullOrEmpty())
            {
                _logger.LogWarning("Session not provided and the code isn't supposed to reach here without a stored session");
                return response;
            }

            BaseListRequest sessionRequest = new BaseListRequest();
            sessionRequest.Filters.Add(new Filter()
            {
                Target1 = "SessionKey",
                OperationType = FilterOperationType.Equals,
                Value1 = session
            });


            var sessionResponse = _sessionRepository.FindByRequest(sessionRequest);
            if (sessionResponse.InError)
            {
                response.Merge(sessionResponse);
                return response;
            }

            if (!sessionResponse.ResponseData.Any())
            {
                response.AddValidationMessage("911", "Sessão inválida");
                return response;
            }

            AuthSession currentSession = sessionResponse.ResponseData.First();

            if(currentSession.LastRenewDate.AddMinutes(15) < DateTime.Now)
            {
                response.AddValidationMessage("911", "Sessão inválida");
                return response;
            }

            currentSession.LastRenewDate = DateTime.Now;
            _sessionRepository.Update(currentSession);

            response.ResponseData = true;
            response.StatusCode = HttpStatusCode.OK;

            _sessionRepository.CloseConnection();
            return response;
        }
    }
}
