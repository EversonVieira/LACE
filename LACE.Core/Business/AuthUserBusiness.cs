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
            model.Password = LoginUtility.EncryptPassword(model.Password);
            return _authUserRepository.Insert(model);
        }

        public Response<bool> Update(AuthUser model)
        {
            model.Password = LoginUtility.EncryptPassword(model.Password);
            return _authUserRepository.Update(model);
        }

        public ListResponse<AuthUser> FindAll()
        {
            return _authUserRepository.FindAll();
        }

        public ListResponse<AuthUser> FindById(long Id)
        {

            BaseListRequest request = new BaseListRequest();

            request.Filters.Add(new Filter()
            {
                Target1 = "UserId",
                OperationType = FilterOperationType.Equals,
                Value1 = Id
            });

            return _authUserRepository.FindByRequest(request);
        }
    }
}
