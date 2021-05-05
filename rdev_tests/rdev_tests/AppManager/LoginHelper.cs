using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace rdev_tests.AppManager
{
    public class LoginHelper : HelperBase
    {
        public string BaseURL { get; set; }
        public string ConnectionString { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }

        public LoginHelper(ApplicationManager manager, string baseURL, string connectionString, string login, string password)
            : base(manager)
        {
            BaseURL = baseURL;
            ConnectionString = connectionString;
            Login = login;
            Password = password;
        }
        
        public void LoginRdev()
        {
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
