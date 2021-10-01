using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace misa.timesheet.core.MyResources
{
    public static class ResourcesController
    {
        // trả về messenger cho người dùng
        public static string GetMessengerErrorUser(string propName, string errorType, string value)
        {
            return MyResources.ResourcePropVN.ResourceManager.GetString(propName) + value +
                    MyResources.ResourceErrorTypeVN.ResourceManager.GetString($"User.{errorType}");
        }

        // trả về messenger cho dev
        public static string GetMessengerErrorDev(string propName, string errorType, string value)
        {
            return propName + value +
                MyResources.ResourceErrorTypeVN.ResourceManager.GetString($"Dev.{errorType}");
        }
    }
}
