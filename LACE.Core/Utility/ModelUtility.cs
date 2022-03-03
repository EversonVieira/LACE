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
                    prop.SetValue(obj, reader[prop.Name]);
                }
            }


            return obj;
        }
    }
}
