using LACE.Core.ExtensionMethods;
using LACE.Core.Models;
using LACE.Core.Repository;
using LACE.Core.Utility;
using Microsoft.Extensions.Logging;
using Nedesk.Core.Models;
using Nedesk.Extensions;
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
            Response<long> response = new Response<long>();

            BaseListRequest request = new BaseListRequest();
            request.Filters.AddRange(new List<Filter>
            {
                new Filter
                {
                    Target1 = nameof(AuthUser.Cpf),
                    OperationType = FilterOperationType.Equals,
                    AggregateType = FilterAggregateType.OR,
                    Value1 = model.Cpf.RemoveDotsAndDashes(),
                },
                new Filter
                {
                    Target1 = nameof(AuthUser.Rg),
                    OperationType = FilterOperationType.Equals,
                    AggregateType = FilterAggregateType.OR,
                    Value1 = model.Rg.RemoveDotsAndDashes(),
                },
                new Filter
                {
                    Target1 = nameof(AuthUser.Sus),
                    OperationType = FilterOperationType.Equals,
                    AggregateType = FilterAggregateType.OR,
                    Value1 = model.Sus.RemoveDotsAndDashes(),
                }
            });

            if (model.Cpf.IsNullOrEmpty() && model.Rg.IsNullOrEmpty() && model.Sus.IsNullOrEmpty())
            {
                response.AddValidationMessage("911", "Informe pelo menos um dos campos a seguir: CPF, RG ou SUS");
                _authUserRepository.CloseConnection();
                return response;
            }

            if (model.Sus.Length > 20)
            {
                response.AddValidationMessage("911", "O tamanho máximo do campo Sus é de 20 caracteres.");
            }
            if (model.Rg.Length > 20)
            {
                response.AddValidationMessage("911", "O tamanho máximo do campo Rg é de 20 caracteres.");
            }
            if (model.Cpf.Length > 20)
            {
                response.AddValidationMessage("911", "O tamanho máximo do campo Cpf é de 20 caracteres.");
            }
            ListResponse<AuthUser> userResponse = _authUserRepository.FindByRequest(request);


            if (!userResponse.IsValid || userResponse.ResponseData.Any())
            {
                response.AddValidationMessage("911", "CPF ou RG já cadastrados na base de dados, se não foi você, entre em contato com o suporte para recuperar acesso.");
                _authUserRepository.CloseConnection();
                return response;
            }


            response =  _authUserRepository.Insert(model);
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
