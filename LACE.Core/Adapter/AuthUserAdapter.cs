using LACE.Core.Business;
using LACE.Core.Models;
using LACE.Core.Models.DTO;
using LACE.Core.Utility;
using Microsoft.Extensions.Logging;
using Nedesk.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Adapter
{
    public class AuthUserAdapter
    {
        private readonly AuthUserBusiness _authUserBusiness;
        private readonly ILogger _logger;
        public AuthUserAdapter(AuthUserBusiness authUserBusiness, ILogger<AuthUserAdapter> logger)
        {
            _authUserBusiness = authUserBusiness;
            _logger = logger;
        }

        public Response<long> Insert(DTO_AuthUser_Register model)
        {
            Response<long> response = new Response<long>();
            if (!model.Password.Equals(model.ConfirmPassword))
            {
                response.AddValidationMessage("911", "Senhas não são iguais");
                return response;
            }

            model.Password = LoginUtility.EncryptPassword(model.Password);
            return _authUserBusiness.Insert(model);
        }

        public Response<bool> Update(DTO_AuthUser_Update model)
        {
            Response<bool> response = new Response<bool>();
            if (!model.Password.Equals(model.ConfirmPassword))
            {
                response.AddValidationMessage("911", "Senhas não correspondem.");
                return response;
            }

            model.Password = LoginUtility.EncryptPassword(model.Password);
            BaseListRequest request = new BaseListRequest();
            request.Filters.AddRange(new List<Filter>
            {
                new Filter
                {
                    Target1 = "Email",
                    OperationType = FilterOperationType.Equals,
                    Value1 = model.Email,
                    AggregateType = FilterAggregateType.AND
                },

                new Filter
                {
                    Target1 = "Password",
                    OperationType = FilterOperationType.Equals,
                    Value1 = model.Password
                }
            });
            var findResponse = _authUserBusiness.FindByRequest(request);

            if (!findResponse.ResponseData.Any())
            {
                response.AddValidationMessage("911", "Não foi possível atualizar o usuário pois a senha anterior não corresponde a última senha.");
                return response;
            }

            return _authUserBusiness.Update(model);
        }
        
    }
}
