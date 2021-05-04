using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium.Interactions;
using LinqToDB.Configuration;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

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
        /// Тест типа данных SysBoolean. Удаление или отмена удаления записи
        /// </summary>
        public void TestCancelDeleteOrSubmitDelete(string type, string action, string click)
        {
            //получаем первый найденный в БД id тестируемого типа
            string recid = manager.Db.GetRecidForTestingType(type);
            //int count = manager.Db.CheckingRowInDb();
            if (recid == "00000000-0000-0000-0000-000000000000")
            {
                //if (type == "sysboolean")
                //{
                //    manager.SysBoolean.SysBooleanTestCreate();
                //    recid = manager.Db.GetRecidForTestingType(type);
                //}
                //else if (type == "sysdate")
                //{
                //    manager.SysDate.SysDateTestCreateDateBox(type);
                //    recid = manager.Db.GetRecidForTestingType(type);
                //}
                if (type == "sysint")
                {
                    string value = "10";
                    manager.SysInt.SysStringTestCreate(value, type);
                    recid = manager.Db.GetRecidForTestingType(type);
                }
                //else if (type == "sysenum")
                //{
                //    int value = 1;
                //    manager.SysEnum.SysEnumTestCreate(value, type);
                //    recid = manager.Db.GetRecidForTestingType(type);
                //}
            }
            manager.Navigation.OpenHomePage();
            ClickDataType();
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
        /// заполнение поля с типом данных SysEnum
        /// </summary>
        public void FillFieldSysEnum(int value)
        {
            string stepInfo = "заполнение поля с типом данных SysEnum";
            manager.WaitShowElement(By.XPath("//input[@name='sysenum_test']/..//div[@class='dx-dropdowneditor-icon']"), stepInfo);
            try
            {
                driver.FindElement(By.XPath("//input[@name='sysenum_test']/..//div[@class='dx-dropdowneditor-icon']")).Click();
                driver.FindElements(By.XPath("//div[@class='dx-scrollview-content']//div[@role='option']"))[value].Click();
            }
            catch
            {
                var el = driver.FindElement(By.XPath("//input[@name='sysenum_test']/..//div[@class='dx-dropdowneditor-icon']"));
                el.Click();
                var el2 = driver.FindElements(By.XPath("//div[@class='dx-scrollview-content']//div[@role='option']"))[value];
                el2.Click();
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
        /// заполнение поля типа sysDate через календарь
        /// </summary>
        /// <returns></returns>
        public string FillFieldSysDateThroughDateBox(string lastDate, string chevron)
        {
            string stepInfo = "Заполнение поля типа sysDate через календарь";
            var dt = manager.Base.GetDateForCompare(lastDate);

            //получаем день текущей даты
            DateTime thisDay = DateTime.Today;
            int dateNow = thisDay.Day;
            string dateNowToString = dateNow.ToString();
            //исключить вероятность отсутствия текущей даты в предыдущем месяце
            if (dateNow == 31 || dateNow == 30 || dateNow == 29)
            {
                dateNow = 28;
            }
            //DateTime MonthToDate = new DateTime();
            ////если предыдущей даты нет - значит это создание записи и передаем значение предыдущего месяца от текущей даты
            //if (chevron == "left")
            //{
            //    MonthToDate = thisDay.AddMonths(-1);
            //}
            ////если предыдущая дата есть - значит это редактирование записи и передаем значение следующего месяца от предыдущей даты
            //else if(chevron == "right")
            //{
            //    MonthToDate = thisDay.AddMonths(+1);
            //}
            //int month = MonthToDate.Month;
            //получаем даты предыдущего месяца текущей даты для корректного поиска даты в календаре
            var dtNew = dt.ToString("yyyy/MM/dd");
            string date = dtNew.Replace(".", "/");

            manager.WaitShowElement(By.XPath("//input[@name='sysdate_test']/..//div[@role='button']"), stepInfo);
            driver.FindElement(By.XPath("//input[@name='sysdate_test']/..//div[@role='button']")).Click();
            manager.WaitShowElement(By.CssSelector($"i.dx-icon-chevron{chevron}"), stepInfo);
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector($"i.dx-icon-chevron{chevron}")).Click();
            manager.WaitShowElement(By.XPath($"//td[@data-value='{date}']//span[contains(text(), '{dateNowToString}')]"), stepInfo);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//td[@data-value='{date}']//span[contains(text(), '{dateNowToString}')]")).Click();
            string dateNew = driver.FindElement(By.XPath("//input[@name='sysdate_test']")).GetAttribute("value");
            return dateNew;
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
        /// Получение информации о состоянии чекбокса типа SysBoolean
        /// </summary>
        /// <returns></returns>
        public bool CheckboxIsActive()
        {
            string stepInfo = "Получение информации о состоянии чекбокса типа SysBoolean";
            manager.WaitShowElement(By.CssSelector("tr[aria-rowindex='1']"), stepInfo);
            return manager.IsActiveElement();
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
        /// клик на чекбокс SysBoolean
        /// </summary>
        public void ClickCheckboxTypeSysboolean()
        {
            string stepInfo = "клик на чекбокс SysBoolean";
            manager.WaitShowElement(By.CssSelector("input[name='sysboolean_test']"), stepInfo);
            try
            {
                var click = driver.FindElement(By.CssSelector("input[name='sysboolean_test']"));
                click.Click();
                Thread.Sleep(500);
            }
            catch
            {
                manager.JSClick(driver.FindElement(By.CssSelector("input[name='sysboolean_test']")));
                Thread.Sleep(500);
            }
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
        public void ClickDataType()
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
