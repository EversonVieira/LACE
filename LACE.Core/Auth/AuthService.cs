using Nedesk.Core.Interfaces;
using Nedesk.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Auth
{
    public class AuthService : IAuth
    {
        public BaseResponse<ISession> CreateSession()
        {
            throw new NotImplementedException();
        }

        public BaseResponse<bool> DropSession()
        {
            throw new NotImplementedException();
        }

        public BaseResponse<IAuthUser> GetSessionUser()
        {
            throw new NotImplementedException();
        }

        public BaseResponse<bool> ValidateSession()
        {
            throw new NotImplementedException();
        }
    }
}
