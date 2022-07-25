using LACE.Core.Models;
using LACE.Core.Repository;
using Microsoft.Extensions.Logging;
using Nedesk.Core.Models;
using Nedesk.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Validators
{
    public class AuthUserValidator
    {
        private readonly AuthUserRepository _authUserRepository;
        private readonly ILogger _logger;

        public AuthUserValidator(AuthUserRepository authUserRepository, 
                                 ILogger<AuthUserValidator> logger)
        {
            _authUserRepository = authUserRepository;
            _logger = logger;
        }

        public void ValidateInsert(NDResponse response, AuthUser model)
        {
            ValidateCommon(response, model);
        }

        public void ValidateUpdate(NDResponse response, AuthUser model)
        {
            if (model.Id <= 0)
            {
                response.AddValidationMessage("911", "Usuário não cadastrado.");
            }

            ValidateCommon(response, model);
        }

        public void ValidateCommon(NDResponse response, AuthUser model)
        {
            if (model.Cpf.IsNullOrEmpty() && model.Rg.IsNullOrEmpty() && model.Sus.IsNullOrEmpty())
            {
                response.AddValidationMessage("911", "Informe pelo menos um dos campos a seguir: CPF, RG ou SUS");
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
        }
    }
}
