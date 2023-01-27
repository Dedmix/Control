using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace _3063User_5.Class
{
    internal class Class2
    {
        private static BD.user6Entities DBConnector;
        private static BD.User UserData;
        public static string Login;

        public static BD.user6Entities GetUser6Entities()
        {
            if (DBConnector == null)
            {
                DBConnector = new BD.user6Entities();
            }
            return DBConnector;
        }
        public static BD.User GetUserDate()
        { return UserData; }
        public static bool Autgorize(string login, string password)
        {
            Login = login.Trim();
            string getpassword = password.Trim();
            UserData = GetUser6Entities().User.Where(us => us.UserLogin == Login && us.UserPassword == getpassword).FirstOrDefault();
            return UserData != null;

        }
    }
}
