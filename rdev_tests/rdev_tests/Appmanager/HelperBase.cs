using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace rdev_tests.AppManager
{
    public class HelperBase
    {
        protected ApplicationManager manager;
        public IWebDriver driver;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
            driver = manager.Driver;
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
