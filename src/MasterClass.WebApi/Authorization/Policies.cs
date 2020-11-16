using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterClass.WebApi.Authorization
{
    public static class Policies
    {
        public const string REQUIRED_SUPERADMIN_ROLE = "RequiredSuperAdminRole";
        public const string REQUIRED_ADMIN_ROLE = "RequiredAdminRole";
        public const string REQUIRED_ALCOHOL_MAJORITY = "RequiredAlcoholMajority";
    }
}
