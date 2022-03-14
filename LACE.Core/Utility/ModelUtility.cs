using Nedesk.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LACE.Core.Utility
{
    public static class ModelUtility
    {
        public static T FillObject<T>(DbDataReader reader)
        {
            T obj = Activator.CreateInstance<T>();

            var properties = typeof(T).GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                if (prop.SetMethod.IsNull()) continue;

                if (reader.GetColumnSchema().ToList().Exists(x => x.ColumnName.Equals(prop.Name)) && reader[prop.Name] != DBNull.Value)
                {
                    prop.SetValue(obj, Convert.ChangeType(reader[prop.Name], prop.PropertyType));
                }
            }


            return obj;
        }

        public static TOutput FromObj<TInput, TOutput>(TInput obj)
        {
            TOutput output = Activator.CreateInstance<TOutput>();

            var outputProperties = typeof(TOutput).GetProperties().ToList();
            var inputProperties = typeof(TInput).GetProperties().ToList();

            foreach (PropertyInfo prop in outputProperties)
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
