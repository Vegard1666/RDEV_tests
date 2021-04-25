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
    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(AppManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void GoToHomePage()
        {
            if (driver.Url == baseURL + "login")
            {
                return;
            }

            driver.Navigate().GoToUrl(baseURL + "login");
        }

        public void GoToStringsTable()
        {
            if (driver.Url == baseURL + "tables/sysstring_test_table")                
            {
                return;
            }
            driver.FindElement(By.LinkText("1. Типы данных")).Click();
            driver.FindElement(By.LinkText("1. SysString")).Click();
        }
    }
}
