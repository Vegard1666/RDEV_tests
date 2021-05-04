using OpenQA.Selenium;
using System;
using System.Threading;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;

namespace rdev_tests.AppManager
{
    public class RdevHelper : HelperBase
    {
        private string baseURL;

        public RdevHelper(ApplicationManager manager, string baseURL)
           : base(manager)
        {
            this.baseURL = baseURL;
        }


        /// <summary>
        /// Удаление или отмена удаления записи
        /// </summary>
        public void TestCancelDeleteOrSubmitDelete(string type, string action, string click)
        {
            //получаем первый найденный в БД id тестируемого типа
            string recid = manager.Db.GetRecidForTestingType(type);
            //int count = manager.Db.CheckingRowInDb();
            if (recid == "00000000-0000-0000-0000-000000000000")
            {                
               string value = "10";
               manager.SysString.SysStringTestCreate(value, type);
               recid = manager.Db.GetRecidForTestingType(type);                               
            }
            manager.Navigation.OpenHomePage();
            ClickDataTypes();
            ClickAllTypes();
            //получение url страницы
            string url = driver.Url;
            OpenFirstRow(click);
            //получаем url страницы с записью тестируемого типа данных
            string urlType = String.Concat(url, @"/", recid);
            driver.Navigate().GoToUrl(urlType);
            ClickDelete(action);
            if (action == "Отмена")
            {
                //проверка что запись не была удалена (recstate=1)
                manager.Base.CheckingRecstate(1, recid);

                //проверяется, что после отмены записи url не изменился и пользователь находится на прежней странице
                Assert.AreEqual(urlType, driver.Url, "Url не равен ожидаемому значению. Пользователь не остался на странице с записью после отмены удаления.");
            }
            else if (action == "Удалить")
            {
                Assert.IsFalse(manager.Db.CheckingRecord(recid), "После удаления запись есть в БД");
            }
        }
        
        /// <summary>
        /// Заполнение поля типа SysString
        /// </summary>
        /// <param name="value"></param>
        public void FillFieldSysString(string value)
        {
            string stepInfo = "Заполнение поля типа данных sysString";
            manager.WaitShowElement(By.XPath("//input[@name='sysstring_test']"), stepInfo);
            try
            {
                var v = driver.FindElement(By.XPath("//input[@name='sysstring_test']")).GetAttribute("value");
                do
                {
                    driver.FindElement(By.XPath("//input[@name='sysstring_test']")).Clear();
                    v = driver.FindElement(By.XPath("//input[@name='sysstring_test']")).GetAttribute("value");
                }
                while (v != "");

                driver.FindElement(By.XPath("//input[@name='sysstring_test']")).SendKeys(value);
            }
            catch
            {
                var el = driver.FindElement(By.XPath("//input[@name='sysstring_test']"));
                el.Clear();
                el.SendKeys(value);
            }
        }

        
        /// <summary>
        /// Нажать кнопку "Редактировать"
        /// </summary>
        public void ClickEdit()
        {
            string stepInfo = "клик на 'Редактировать'";
            manager.WaitShowElement(By.XPath("//div[@role='toolbar']//button[@type='button']"), stepInfo);
            Thread.Sleep(1000);
            try
            {
                var click = driver.FindElements(By.XPath("//div[@role='toolbar']//button[@type='button']"))[1];
                click.Click();
                Thread.Sleep(500);
            }
            catch
            {
                manager.JSClick(driver.FindElements(By.XPath("//div[@role='toolbar']//button[@type='button']"))[1]);
                Thread.Sleep(500);
            }
            manager.WaitShowElement(By.CssSelector("div.card-body"), stepInfo);
            Thread.Sleep(1000);
        }
        /// <summary>
        /// Выделить первую запись
        /// </summary>
        public void ClickFirstRow()
        {
            string stepInfo = "Выделить первую запись";
            manager.WaitShowElement(By.CssSelector("tr[aria-rowindex='1']"), stepInfo);
            driver.FindElement(By.CssSelector("tr[aria-rowindex='1']")).Click();
            Thread.Sleep(500);
        }

        /// <summary>
        /// Заполнить поле типа данных sysDate
        /// </summary>
        public void FillFieldSysDate(string value)
        {
            string stepInfo = "Заполнение поля типа данных sysDate";
            manager.WaitShowElement(By.XPath("//input[@name='sysdate_test']/..//input[@class='dx-texteditor-input']"), stepInfo);
            try
            {
                driver.FindElement(By.XPath("//input[@name='sysdate_test']/..//input[@class='dx-texteditor-input']")).SendKeys(value);
            }
            catch
            {
                var el = driver.FindElement(By.XPath("//input[@name='sysdate_test']/..//input[@class='dx-texteditor-input']"));
                el.SendKeys(value);
            }
        }


        /// <summary>
        /// Удаление или отмена удаления записи
        /// </summary>
        public void ClickDelete(string action)
        {
            string stepInfo = "Удаление или отмена удаления записи";
            manager.WaitShowElement(By.XPath("//button[@type='button' and contains(text(), ' Удалить')]"), stepInfo);
            var click = driver.FindElement(By.XPath("//button[@type='button' and contains(text(), ' Удалить')]"));
            click.Click();
            manager.WaitShowElement(By.XPath("//div[@class='modal-header']//h5[contains(text(), 'Внимание!')]"), stepInfo);
            manager.WaitShowElement(By.XPath("//div[contains(text(), 'Вы действительно хотите удалить запись?')]"), stepInfo);
            var click2 = driver.FindElement(By.XPath($"//div[@class='modal-footer']//button[contains(text(), '{action}')]"));
            click2.Click();
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Открытие первой записи в таблице через двойной клик или ПКМ > "Открыть"
        /// </summary>
        public void OpenFirstRow(string click)
        {
            Actions action = new Actions(driver);
            string stepInfo = "Клик по первой записи в таблице";
            manager.WaitShowElement(By.CssSelector("tr[aria-rowindex='1']"), stepInfo);
            //открыть через двойной клик
            if (click == "double")
            {
                try
                {
                    IWebElement locator = driver.FindElement(By.CssSelector("tr[aria-rowindex='1']"));
                    var el = action.DoubleClick(locator);
                    el.Perform();
                }
                catch
                {
                    IWebElement locator = driver.FindElement(By.CssSelector("tr[aria-rowindex='1']"));
                    var el = action.DoubleClick(locator);
                    el.Perform();
                }
            }
            //открыть через ПКМ > "Открыть"
            else if (click == "right")
            {
                IWebElement locator = driver.FindElement(By.CssSelector("tr[aria-rowindex='1']"));
                action.ContextClick(locator).Perform();
                string selector = "//li[@class='dx-menu-item-wrapper dx-menu-last-group-item']";
                manager.WaitShowElement(By.XPath(selector), stepInfo);
                manager.ElementIsClickable(driver.FindElement(By.XPath(selector)));
                var el = driver.FindElement(By.XPath(selector));
                el.Click();
            }
        }     

        /// <summary>
        /// Нажать 'Сохранить' у записи (кнопка сверху)
        /// </summary>
        public void SaveСhangesNote()
        {
            try
            {
                var click = driver.FindElements(By.CssSelector("button[type='submit']"))[0];
                click.Click();
                Thread.Sleep(500);
            }
            catch
            {
                manager.JSClick(driver.FindElements(By.CssSelector("button[type='submit']"))[0]);
                Thread.Sleep(500);
            }
            Thread.Sleep(3000);
        }        

        /// <summary>
        /// клик на 'Добавить'
        /// </summary>
        public void ClickAdd()
        {
            string stepInfo = "клик на 'Добавить'";
            manager.WaitShowElement(By.XPath("//button[@type='button' and contains(text(), 'Добавить')]"), stepInfo);
            try
            {
                var click = driver.FindElement(By.XPath("//button[@type='button' and contains(text(), 'Добавить')]"));
                click.Click();
                Thread.Sleep(500);
            }
            catch
            {
                manager.JSClick(driver.FindElement(By.XPath("//button[@type='button' and contains(text(), 'Добавить')]")));
                Thread.Sleep(500);
            }
            manager.WaitShowElement(By.CssSelector("div.card-body"), stepInfo);
            Thread.Sleep(1000);
        }

        /// <summary>
        /// клик на таблицу 'Все типы'
        /// </summary>
        public void ClickAllTypes()
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

        /// <summary>
        /// клик на таблицу 'Типы данных'
        /// </summary>
        public void ClickDataTypes()
        {
            string stepInfo = "клик на таблицу 'Типы данных'";
            manager.WaitShowElement(By.XPath("//a[contains(text(), 'Типы данных')]"), stepInfo);
            try
            {
                var click = driver.FindElement(By.XPath("//a[contains(text(), 'Типы данных')]"));
                click.Click();
                Thread.Sleep(500);
            }
            catch
            {
                manager.JSClick(driver.FindElement(By.XPath("//a[contains(text(), 'Типы данных')]")));
                Thread.Sleep(500);
            }
            Thread.Sleep(500);
        }
    }
}
