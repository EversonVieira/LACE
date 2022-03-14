using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string RemoveDotsAndDashes(this string value)
        {
            return value.Replace(".", "").Replace("-","");
        }
    }
}
