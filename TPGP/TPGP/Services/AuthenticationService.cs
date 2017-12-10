using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;

namespace TPGP.Services
{
    public class AuthenticationService
    {
        public void LogIn(String username, Boolean createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(username, createPersistentCookie);
        }

        public void LogOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}