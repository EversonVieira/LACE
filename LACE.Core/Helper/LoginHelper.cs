using LACE.Core.Helper.Configuration;
using LACE.Core.Models;
using Microsoft.Extensions.Logging;
using Nedesk.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Helper
{
    public class LoginHelper
    {
        private readonly LoginHelperConfiguration _configuration;
        private readonly ILogger _logger;

        public LoginHelper(LoginHelperConfiguration configuration,
                            ILogger<LoginHelper> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }


        public void EncrpytUserSensistiveInformation(AuthUser user)
        {
            EncrpytUserSensitiveInformationImplementation(user);
        }

        private void EncrpytUserSensitiveInformationImplementation(AuthUser user)
        {
            user.Password = NDEncrypter.GetEncrpytString(user.Password,
                                                        false,
                                                        _configuration.Algorithm,
                                                        _configuration.Secret);
        }


    }
}
