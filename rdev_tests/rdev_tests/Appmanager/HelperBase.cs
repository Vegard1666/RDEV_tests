using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace rdev_tests.AppManager
{
    public class HelperBase
    {
        protected ApplicationManager manager;
        protected IWebDriver driver;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
            driver = manager.Driver;
        }
        /// <summary>
        /// метод заполнения пустого поля
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="text"></param>
        public void Type(By locator, string text)
        {
            if (text != null)
            {
                driver.FindElement(locator).Clear();
                driver.FindElement(locator).SendKeys(text);
            }
        }
        /// <summary>
        /// Проверяется, что recstate равен ожидаемому значению
        /// </summary>
        /// <param name="v"></param>
        /// <param name="recid"></param>
        public void CheckingRecstate(int v, string recid)
        {
            int recstate = manager.Db.CheckRecstateInDb(recid);
            Assert.AreEqual(v, recstate, $"Ошибка! В БД recstate={recstate}. Ожидалось - recstate={v}");
        }

        /// <summary>
        /// Клик по элементу из списка
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="text"></param>
        /// <param name="locatorXpath"></param>
        public void SelectElementType(By locator, string text, By locatorXpath)
        {
            if (text != null)
            {
                new SelectElement(driver.FindElement(locator)).SelectByText(text);
                driver.FindElement(locatorXpath).Click();
            }
        }
        /// <summary>
        /// Получение информации о наличии элемента
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
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
        
        /// <summary>
        /// получение идентификатора записи с URL
        /// </summary>
        public string GetRecid()
        {
            string currentURL = driver.Url;
            string id = currentURL.Split(@"/")[5];
            return id;
        }       
    }
}
