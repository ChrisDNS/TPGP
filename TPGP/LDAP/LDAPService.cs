using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDAP
{
    public sealed class LDAPService
    {
        private static readonly Lazy<LDAPService> INSTANCE = new Lazy<LDAPService>(() => new LDAPService());
        private List<LDAPUser> users = new List<LDAPUser>();

        public static LDAPService Instance
        {
            get
            {
                return INSTANCE.Value;
            }
        }

        private LDAPService()
        {
            AddUsers();
        }

        private void AddUsers()
        {
            users.Add(new LDAPUser("Pierre", "Pierre", "Pierre", "Pierre", "pierre@pierre.pierre", "Pierriste", "17 rue Pierre"));
            users.Add(new LDAPUser("Sarra", "Sarra", "Sarra", "Sarra", "sarra@sarra.sarra", "sarreuse", "17 rue Sarra"));
        }

        public LDAPUser AuthenticationAndIdentification(string login, string password)
        {
            LDAPUser user = null;
            if ((user = users.Find(u => u.Username == login)) == null) return null;
            else
            {
                if (user.Password == password)
                    return user;
            }

            return null;
        }
    }
}