using Nedesk.Core.Interfaces;
using Nedesk.Core.Models;

namespace LACE.Core.Models
{
    public class AuthUser : BaseUser, IAuthUser
    {
        public string Cpf { get; set; }
        public string Rg { get; set; }
    }

}
