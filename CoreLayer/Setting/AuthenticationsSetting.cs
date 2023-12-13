using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Setting
{
    public static class AuthenticationsSetting
    {
        public static int AuthorizationCodeDigits = 5;
        public static TimeSpan UserFreePlan = TimeSpan.FromDays(14);
    }
}
