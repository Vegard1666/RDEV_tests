using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using rdev_tests.AppManager;
using rdev_tests.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace rdev_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;
        protected string login;
        protected string password;
        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();
        protected LoginHelper loginHelper;
        protected NavigationHelper navigationHelper;
        protected RdevHelper rdevHelper;
        //protected SysBooleanHelper sysBooleanHelper;
        //protected SysDateHelper sysDateHelper;
        protected SysStringHelper sysIntHelper;
        //protected SysEnumHelper sysEnumHelper;
        protected DbHelper dbHelper;
        protected HelperBase baseHelper;
        private string connectionString;

        private ApplicationManager(SettingsJson settings)
        {

            var profile = new ChromeOptions();
            profile.AddExtension(@"C:\Users\i.lebedev\AppData\Local\Google\Chrome\User Data\Default\Extensions\iifchhfnnmpdbibifmljnfjhpififfog\1.2.8_0.crx");
            driver = new ChromeDriver(profile);
            baseURL = settings.Rdev.Url;
            login = settings.Rdev.Login;
            password = settings.Rdev.Password;
            driver.Manage().Window.Maximize();
            connectionString = settings.Rdev.ConnectionString;
            loginHelper = new LoginHelper(this, baseURL, connectionString, login, password);
            navigationHelper = new NavigationHelper(this, baseURL, login, password);
            rdevHelper = new RdevHelper(this, baseURL);
            dbHelper = new DbHelper(this, connectionString);
            baseHelper = new HelperBase(this);
            //sysBooleanHelper = new SysBooleanHelper(this, baseURL);
            //sysDateHelper = new SysDateHelper(this, baseURL);
            //sysEnumHelper = new SysEnumHelper(this, baseURL);
            sysIntHelper = new SysStringHelper(this, baseURL);
        }
        public static ApplicationManager GetInstance(SettingsJson settings)
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager NewInstance = new ApplicationManager(settings);
                //NewInstance.Navigation.OpenHomePage();
                app.Value = NewInstance;

            }
            return app.Value;
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }
        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }
        public HelperBase Base
        {
            get
            {
                return baseHelper;
            }
        }
        public RdevHelper Rdev
        {
            get
            {
                return rdevHelper;
            }
        }
        //public SysBooleanHelper SysBoolean
        //{
        //    get
        //    {
        //        return sysBooleanHelper;
        //    }
        //}
        //public SysDateHelper SysDate
        //{
        //    get
        //    {
        //        return sysDateHelper;
        //    }
        //}
        public SysStringHelper SysInt
        {
            get
            {
                return sysIntHelper;
            }
        }
        //public SysEnumHelper SysEnum
        //{
        //    get
        //    {
        //        return sysEnumHelper;
        //    }
        //}
        public DbHelper Db
        {
            get
            {
                return dbHelper;
            }
        }
        public NavigationHelper Navigation
        {
            get
            {
                return navigationHelper;
            }
        }
        /// <summary>
        /// ожидание появления элемента
        /// </summary>
        /// <param name="iClassName"></param>
        /// <param name="stepInfo"></param>
        public void WaitShowElement(By iClassName, string stepInfo)
        {
            try
            {
                WebDriverWait iWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                iWait.Until(ExpectedConditions.ElementIsVisible(iClassName));
            }
            catch (Exception)
            {
                Assert.Fail($"Не найден ожидаемый элемент: '{iClassName}', время ожидания: 60сек. Шаг: {stepInfo}"); ;
            }
        }
        /// <summary>
        /// ожидание скрытия элемента
        /// </summary>
        /// <param name="iClassName"></param>
        /// <param name="stepInfo"></param>
        public void WaitHideElement(By iClassName, string stepInfo)
        {
            try
            {
                WebDriverWait iWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                iWait.Until(ExpectedConditions.InvisibilityOfElementLocated(iClassName));
            }
            catch (Exception)
            {
                Assert.Fail($"Отображение троббера длилось более 60сек. Шаг: {stepInfo}");
            }
        }
        /// <summary>
        /// Скролл до видимости элемента
        /// </summary>
        /// <param name="selector"></param>
        public void ScrollToView(IWebElement selector)
        {
            Actions actions = new Actions(driver);

            actions.MoveToElement(selector);

            actions.Perform();
        }
        /// <summary>
        /// Клик через JavaScript
        /// </summary>
        /// <param name="selector"></param>
        public void JSClick(IWebElement selector)
        {
            var webElement = selector;
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", webElement);
        }
        /// <summary>
        /// Получение информации о кликабельности элемента
        /// </summary>
        /// <param name="selector"></param>
        public void ElementIsClickable(IWebElement selector)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(selector));
        }
        /// <summary>
        /// получение информации о чекбоксе из интерфекса (включен/выключен)
        /// </summary>
        /// <returns></returns>
        public bool IsActiveElement()
        {
            String script = "return window.getComputedStyle(document.querySelector('span.dx-checkbox-icon'),':before').getPropertyValue('position')";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            string content = (String)js.ExecuteScript(script);
            if (content == "absolute")
            {
                return true;
            }
            return false;
        }
    }
}
