using _3063User_5.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3063User_5.Class
{
    public static class Class1
    {
        private static Authorization authorization;
        private static AdminCatalog adminCatalog;
        public static UserCatalog userCatalog;

        public static Authorization GetAuthorization()
        {
            if (authorization == null)
            {
                authorization = new Authorization();
            }
            return authorization;
        }
        public static AdminCatalog GetAdminCatalog()
        {
            if (adminCatalog == null)
            {
                adminCatalog = new AdminCatalog();
            }
            return adminCatalog;
        }
        public static UserCatalog GetUserCatalog()
        {
            if (userCatalog == null)
            {
                userCatalog = new UserCatalog();
            }
            return userCatalog;
        }

    }
}
