using OpenQA.Selenium;
using System;
using System.Threading;


namespace rdev_tests.AppManager
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;
        private string Login;
        private string Password;

        public NavigationHelper(ApplicationManager manager, string baseURL, string login, string password)
            : base(manager)
        {
            this.baseURL = baseURL;
            Login = login;
            Password = password;
        }
        public void OpenHomePage()
        {
            if (driver.Url == String.Concat(baseURL, "/login"))
            {
                return;
            }
            driver.Navigate().GoToUrl(String.Concat(baseURL, "/login"));
            if (OpenLoginPage())
            {
                manager.Auth.LoginRdev();
            }
        }
        public bool OpenLoginPage()
        {
            Thread.Sleep(1000);
            return IsElementPresent(By.Id("auth_logo"));
        }      
    }
}
