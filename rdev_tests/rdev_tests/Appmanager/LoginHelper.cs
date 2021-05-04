using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

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

        //авторизация в рдев
        public void Auth()
        {
            string stepInfo = "Авторизация пользователя";
            driver.Navigate().GoToUrl(BaseURL);
            manager.WaitHideElement(By.CssSelector("input[placeholder='Логин']"), stepInfo);
            Type(By.CssSelector("input[placeholder='Логин']"), Login);
            Type(By.CssSelector("input[placeholder='Пароль']"), Password);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }   
    }
}
