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
        public Response<ISession> CreateSession()
        {
            throw new NotImplementedException();
        }

        public Response<bool> DropSession()
        {
            throw new NotImplementedException();
        }

        public Response<IAuthUser> GetSessionUser()
        {
            throw new NotImplementedException();
        }

        public Response<bool> ValidateSession()
        {
            throw new NotImplementedException();
        }
    }
}
