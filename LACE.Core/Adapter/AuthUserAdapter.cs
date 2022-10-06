using LACE.Core.Business;
using LACE.Core.ExtensionMethods;
using LACE.Core.Helper;
using LACE.Core.Messages;
using LACE.Core.Models;
using LACE.Core.Models.DTO;
using LACE.Core.Utility;
using Microsoft.Extensions.Logging;
using Nedesk.Core.Enums;
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
        private readonly LoginHelper _loginHelper;
        private readonly ILogger _logger;
        public AuthUserAdapter(AuthUserBusiness authUserBusiness, 
                               LoginHelper helper, 
                               ILogger<AuthUserAdapter> logger)
        {
            _authUserBusiness = authUserBusiness;
            _loginHelper = helper;
            _logger = logger;
        }

        private void MaintainUser(AuthUser model)
        {
            model.Cpf = model.Cpf?.RemoveDotsAndDashes() ?? string.Empty;
            model.Rg = model.Rg?.RemoveDotsAndDashes() ?? string.Empty;
            model.Sus = model.Sus?.RemoveDotsAndDashes() ?? string.Empty;
        }

        public NDResponse<long> Insert(DTO_AuthUser_Register model)
        {
            MaintainUser(model);
            NDResponse<long> NDResponse = new NDResponse<long>();
            if (!model.Password.Equals(model.ConfirmPassword))
            {
                NDResponse.AddValidationMessage(MessageCodesList.Get("LCEAUTH002"));
                return NDResponse;
            }


            _loginHelper.EncrpytUserSensistiveInformation(model);

            return _authUserBusiness.Insert(model);
        }

        public NDResponse<bool> Update(DTO_AuthUser_Update model)
        {
            MaintainUser(model);
            NDResponse<bool> NDResponse = new NDResponse<bool>();
            if (!model.Password.Equals(model.ConfirmPassword))
            {
                NDResponse.AddValidationMessage(MessageCodesList.Get("LCEAUTH002"));
                return NDResponse;
            }

            _loginHelper.EncrpytUserSensistiveInformation(model);
            NDListRequest request = new NDListRequest();
            request.Filters.AddRange(new List<NDFilter>
            {
                new NDFilter
                {
                    Target1 = "Email",
                    OperationType = NDFilterOperationTypeEnum.Equals,
                    Value1 = model.Email,
                    AggregateType = NDFilterAggregateTypeEnum.AND
                },

                new NDFilter
                {
                    Target1 = "Password",
                    OperationType = NDFilterOperationTypeEnum.Equals,
                    Value1 = model.Password
                }
            });
            var findNDResponse = _authUserBusiness.FindByRequest(request);

            if (!findNDResponse.ResponseData.Any())
            {
                NDResponse.AddValidationMessage(MessageCodesList.Get("LCEAUTH003"));
                return NDResponse;
            }

            model.Id = findNDResponse.ResponseData.First().Id;

            return _authUserBusiness.Update(model);
        }
        
    }
}
