using LACE.Core.ExtensionMethods;
using LACE.Core.Models;
using LACE.Core.Repository;
using LACE.Core.Utility;
using LACE.Core.Validators;
using Microsoft.Extensions.Logging;
using Nedesk.Core.DataBase.Factory;
using Nedesk.Core.Enums;
using Nedesk.Core.Models;
using Nedesk.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Business
{
    public class AuthUserBusiness
    {
        private readonly AuthUserRepository _authUserRepository;
        private readonly AuthUserValidator _authUserValidator;
        private readonly NDIDBConnectionFactory _connectionFactory;
        private readonly ILogger _logger;

        public AuthUserBusiness(AuthUserRepository authUserRepository,
                                AuthUserValidator authUserValidator,
                                NDIDBConnectionFactory connectionFactory,
                                ILogger<AuthUserBusiness> logger)
        {
            _authUserRepository = authUserRepository;
            _authUserValidator = authUserValidator;
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public NDResponse<long> Insert(AuthUser model)
        {
            NDResponse<long> response = new NDResponse<long>();

            NDListRequest request = new NDListRequest();
            request.Filters.AddRange(new List<NDFilter>
            {
                new NDFilter
                {
                    Target1 = nameof(AuthUser.Cpf),
                    OperationType = NDFilterOperationTypeEnum.Equals,
                    AggregateType = NDFilterAggregateTypeEnum.OR,
                    Value1 = model.Cpf.RemoveDotsAndDashes(),
                },
                new NDFilter
                {
                    Target1 = nameof(AuthUser.Rg),
                    OperationType = NDFilterOperationTypeEnum.Equals,
                    AggregateType = NDFilterAggregateTypeEnum.OR,
                    Value1 = model.Rg.RemoveDotsAndDashes(),
                },
                new NDFilter
                {
                    Target1 = nameof(AuthUser.Sus),
                    OperationType = NDFilterOperationTypeEnum.Equals,
                    AggregateType = NDFilterAggregateTypeEnum.OR,
                    Value1 = model.Sus.RemoveDotsAndDashes(),
                }
            });
            
            _authUserValidator.ValidateInsert(response, model);
            if (response.HasAnyMessages)
            {
                return response;
            }


            using (DbConnection connection = _connectionFactory.GetReadWriteConnection())
            {
                NDListResponse<AuthUser> userNDResponse = _authUserRepository.FindByRequest(connection, request);


                if (!userNDResponse.IsValid || userNDResponse.ResponseData.Any())
                {
                    response.AddValidationMessage("911", "CPF ou RG já cadastrados na base de dados, se não foi você, entre em contato com o suporte para recuperar acesso.");
                    return response;
                }


                response = _authUserRepository.Insert(connection, model);
                return response;

            }
        }

        public NDResponse<bool> Update(AuthUser model)
        {
            NDResponse<bool> response = new NDResponse<bool>();
            _authUserValidator.ValidateUpdate(response, model);

            using (DbConnection connection = _connectionFactory.GetReadWriteConnection())
            {
                response = _authUserRepository.Update(connection, model);
                return response;
            }
        }

        public NDListResponse<AuthUser> FindAll()
        {
            using (DbConnection connection = _connectionFactory.GetReadOnlyConnection())
            {
                NDListResponse<AuthUser> response = _authUserRepository.FindAll(connection);
                return response;
            }

        }

        public NDListResponse<AuthUser> FindById(long Id)
        {
            NDListResponse<AuthUser> response = new NDListResponse<AuthUser>();

            NDListRequest request = new NDListRequest();

            request.Filters.Add(new NDFilter()
            {
                Target1 = "Id",
                OperationType = NDFilterOperationTypeEnum.Equals,
                Value1 = Id
            });

            using (DbConnection connection = _connectionFactory.GetReadOnlyConnection())
            {
                response = _authUserRepository.FindByRequest(connection, request);
                return response;
            }

        }

        public NDListResponse<AuthUser> FindByRequest(NDListRequest request)
        {
            using (DbConnection connection = _connectionFactory.GetReadOnlyConnection())
            {
                NDListResponse<AuthUser> response = _authUserRepository.FindByRequest(connection, request);
                return response;
            }

        }
    }
}
