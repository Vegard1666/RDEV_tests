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
            Thread.Sleep(500);
            return IsElementPresent(By.Id("auth_logo"));
        }
        /// <summary>
        /// клик на таблицу 'Типы данных'
        /// </summary>
        public void GoToDataTypesMenu()
        {
            string stepInfo = "клик на таблицу 'Типы данных'";
            manager.WaitShowElement(By.XPath("//a[contains(text(), 'Типы данных')]"), stepInfo);
            try
            {
                var click = driver.FindElement(By.XPath("//a[contains(text(), 'Типы данных')]"));
                click.Click();
                Thread.Sleep(200);
            }
            catch
            {
                manager.JSClick(driver.FindElement(By.XPath("//a[contains(text(), 'Типы данных')]")));
                Thread.Sleep(200);
            }
            Thread.Sleep(200);
        }
        /// <summary>
        /// клик на таблицу 'Все типы'
        /// </summary>
        public void GoToTypesTable()
        {
            string stepInfo = "клик на таблицу 'Все типы'";
            try
            {
                var click = driver.FindElement(By.XPath("//a[contains(text(), 'Все типы')]"));
                click.Click();
                Thread.Sleep(500);
            }
            catch
            {
                manager.JSClick(driver.FindElement(By.XPath("//a[contains(text(), 'Все типы')]")));
                Thread.Sleep(500);
            }
            manager.WaitShowElement(By.CssSelector("div.card-header"), stepInfo);
        }
    }
}
