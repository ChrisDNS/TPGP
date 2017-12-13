using LDAP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace TPGPTests.LDAPTests
{
    [TestClass]
    public class LDAPTests
    {
        [TestMethod]
        public void AuthenticationAndIdentification_AuthFakeUser_ReturnNull()
        {
            LDAPUser u = LDAPService.Instance.AuthenticationAndIdentification("fakeUsername", "fakePassword");
            Assert.IsNull(u);
        }

        [TestMethod]
        public void AuthenticationAndIdentification_AuthNotFakeUser_ReturnUser()
        {
            LDAPUser u = LDAPService.Instance.AuthenticationAndIdentification("Sarra", "Sarra");
            Assert.IsNotNull(u);
        }
    }
}
