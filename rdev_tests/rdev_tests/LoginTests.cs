using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace rdev_tests
{
    [TestFixture]

    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredantials()
        {
            app.Auth.Logout();

            AccountData account = new AccountData("admin", "admin");
            app.Auth.Login(account);

            Assert.IsTrue(app.Auth.IsLoggedIn());
        }

        [Test]
        public void LoginWithInvalidCredantials()
        {
            app.Auth.Logout();

            AccountData account = new AccountData("admin", "123456");
            app.Auth.Login(account);

            Assert.IsFalse(app.Auth.IsLoggedIn());
        }
    }
}
