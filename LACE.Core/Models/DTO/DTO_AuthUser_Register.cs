using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Models.DTO
{
    public class DTO_AuthUser_Register:AuthUser
    {
        public string ConfirmPassword { get; set; }
    }
}
