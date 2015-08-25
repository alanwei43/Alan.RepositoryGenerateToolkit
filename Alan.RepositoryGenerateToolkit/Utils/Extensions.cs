using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Alan.RepositoryGenerateToolkit.Utils
{
    public static class Extensions
    {
        public static string UpperFirstLetter(this string str)
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            var newStr = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                if (i == 0)
                {
                    newStr.Append(str[i].ToString().ToUpper());
                }
                else
                {
                    newStr.Append(str[i]);
                }
            }
            return newStr.ToString();
        }

        public static string ToJson(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }
        public static T ToModel<T>(this string str)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(str);
        }
    }
}
