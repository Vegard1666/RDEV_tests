using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
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
                LoginRdev();
            }
        }
        public bool OpenLoginPage()
        {
            Thread.Sleep(1000);
            return IsElementPresent(By.Id("auth_logo"));
        }
        //проверка на необходимость авторизации, если нет - авторизация
        public void LoginRdev()
        {
            string stepInfo = "проверка на необходимость авторизации, если пользователь не авторизован - авторизация";
            Thread.Sleep(1000);
            if (IsLoginIn() == false)
            {
                Auth();
            }
        }
        public bool IsLoginIn()
        {
            bool login = IsElementPresent(By.CssSelector("a.navbar-brand"));
            return login;
        }
        //авторизация в рдев
        public void Auth()
        {
            string stepInfo = "Авторизация пользователя";
            manager.WaitShowElement(By.CssSelector("input[placeholder='Логин']"), stepInfo);
            driver.FindElement(By.CssSelector("input[placeholder='Логин']")).SendKeys(Login);
            driver.FindElement(By.CssSelector("input[placeholder='Пароль']")).SendKeys(Password);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            manager.WaitShowElement(By.CssSelector("a.navbar-brand"), stepInfo); // тут нужно придумать что-то другое, так как в разных сборках может не быть этого элемента
        }

    }
}
