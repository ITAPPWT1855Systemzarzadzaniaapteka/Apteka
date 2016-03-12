using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apteka
{
    public static class AppConfig
    {
        public static class defaultUser
        {
            public static string UserName = "admin1";
            public static string Role = "admin";
            public static string Email = "admin@admin.pl";
            public static string Password = "TestowyAdmin";
        }

        public static string[] defaultRoles = new string[] {
            "Admin",
            "Employee"
        };
    }
}