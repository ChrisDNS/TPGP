using System;
using System.Collections.Generic;

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
            users.Add(new LDAPUser("Pierre", "Pierre", "Pierre", "Pierre", "pierre@pierre.pierre", "Pierriste", "17 rue Pierre", "France"));
            users.Add(new LDAPUser("Sidi", "Sidi", "Sidi", "Sidi", "sidi@sidi.sidi", "Sidouche", "17 rue Sidi", "Algérie"));
            users.Add(new LDAPUser("Sidi2", "Sidi2", "Sidi2", "Sidi2", "sidi2@sidi2.sidi2", "Sidouche", "17 rue Sidi2", "Algérie"));
            users.Add(new LDAPUser("Sarra", "Sarra", "Sarra", "Sarra", "sarra@sarra.sarra", "sarreuse", "17 rue Sarra", "France"));
            users.Add(new LDAPUser("Random", "Random", "Random", "Random", "random@random.random", "randouse", "17 rue Random", "Allemagne"));
            users.Add(new LDAPUser("Chris", "Chris", "Chris", "Chris", "chris@chris.chris", "chris", "17 rue Chris", "France"));
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