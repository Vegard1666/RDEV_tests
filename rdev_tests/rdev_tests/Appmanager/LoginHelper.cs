using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace rdev_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(AppManager manager) : base(manager)
        {
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
               Logout();
            }
            Type(By.XPath("//input[@type='text']"), account.Username);
            Type(By.XPath("//input[@type='password']"), account.Password);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.XPath("//img[@alt='admin@nvx']"));                
        }
        // Нужно доработать (в отладчике проходит нормально, при выполнении падает, вероятно не успевает подтянуть значение).
        public bool IsLoggedInByAdmin()
        {
            return IsLoggedIn()
                && GetLoggetUserName() == "Administrator";
        }

        private string GetLoggetUserName()
        {
            string text = driver.FindElement(By.LinkText("Administrator")).Text;
            return text;
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.XPath("//img[@alt='admin@nvx']")).Click();
                driver.FindElement(By.XPath("(//button[@type='button'])[3]")).Click();
            }
        }
    }
}
