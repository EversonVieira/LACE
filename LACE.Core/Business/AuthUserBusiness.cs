using LACE.Core.Models;
using LACE.Core.Repository;
using LACE.Core.Utility;
using Microsoft.Extensions.Logging;
using Nedesk.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Business
{
    public class AuthUserBusiness
    {
        private readonly AuthUserRepository _authUserRepository;
        private readonly ILogger _logger;

        public AuthUserBusiness(AuthUserRepository authUserRepository, ILogger<AuthUserBusiness> logger)
        {
            _authUserRepository = authUserRepository;
            _logger = logger;
        }

        public Response<long> Insert(AuthUser model)
        {
            Response<long> response =  _authUserRepository.Insert(model);
            _authUserRepository.CloseConnection();

            return response;
        }

        public Response<bool> Update(AuthUser model)
        {
            Response<bool> response = _authUserRepository.Update(model);
            _authUserRepository.CloseConnection();

            return response;
        }

        public ListResponse<AuthUser> FindAll()
        {
            ListResponse<AuthUser> response =  _authUserRepository.FindAll();

            _authUserRepository.CloseConnection();

            return response;

        }

        public ListResponse<AuthUser> FindById(long Id)
        {
            ListResponse<AuthUser> response = new ListResponse<AuthUser>();

            BaseListRequest request = new BaseListRequest();

            request.Filters.Add(new Filter()
            {
                Target1 = "Id",
                OperationType = FilterOperationType.Equals,
                Value1 = Id
            });

            response = _authUserRepository.FindByRequest(request);

            _authUserRepository.CloseConnection();

            return response;
        }

        public ListResponse<AuthUser> FindByRequest(BaseListRequest request)
        {
            ListResponse<AuthUser> response = _authUserRepository.FindByRequest(request);

            _authUserRepository.CloseConnection();
            return response;
        }
    }
}
