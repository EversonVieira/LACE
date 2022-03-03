using LACE.Core.Models;
using LACE.Core.Models.DTO;
using Nedesk.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.ExtensionMethods
{
    public static class ModelExtension
    {
        public static TOutput FromObj<TInput, TOutput>(TInput obj)
        {
            TOutput output = Activator.CreateInstance<TOutput>();

            var outputProperties = typeof(TOutput).GetProperties().ToList();
            var inputProperties = typeof(TInput).GetProperties().ToList();

            foreach(PropertyInfo prop in outputProperties)
            {
                if (prop.SetMethod.IsNotNull() && inputProperties.Exists(x => x.Name.Equals(prop.Name)))
                {
                    prop.SetValue(output, inputProperties.FirstOrDefault(x => x.Name.Equals(prop.Name)).GetValue(obj));
                }
            }
            
            return output;
        }

        
    }
}
