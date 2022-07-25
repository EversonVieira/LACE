using Nedesk.Core.Interfaces;
using Nedesk.Core.Models;

namespace LACE.Core.Models
{
    public class AuthUser : NDBaseUser
    {
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Sus { get; set; }
    }

}
