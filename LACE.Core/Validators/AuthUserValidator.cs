using LACE.Core.Models;
using LACE.Core.Repository;
using Microsoft.Extensions.Logging;
using Nedesk.Core.Interfaces;
using Nedesk.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Validators
{
    public class AuthUserValidator
    {
        private readonly AuthUserRepository _userRepository;
        private readonly ILogger _logger;

        public AuthUserValidator(AuthUserRepository userRepository, ILogger<AuthUserValidator> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public void ValidateInsert(IResponse<AuthUser> response)
        {

        }

        public void ValidateUpdate(IResponse<AuthUser> response)
        {

        }

        public void ValidateDelete(IResponse<AuthUser> response)
        {

        }

        public void ValidateSelect(IResponse<AuthUser> response, BaseListRequest request)
        {

        }

        private void ValidateCommon(IResponse<AuthUser> response)
        {

        }

    }
}
