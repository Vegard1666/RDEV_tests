using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;


namespace rdev_tests.AppManager
{
    public class SysStringHelper : HelperBase
    {

        private string baseURL;
        public SysStringHelper(ApplicationManager manager, string baseURL)
           : base(manager)
        {
            this.baseURL = baseURL;
        }

        public static Random rnd = new Random();
        public string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 223)));
            }
            return builder.ToString();
        }
        /// <summary>
        /// Тест типа данных SysString. Добавление записи
        /// </summary>
        /// <param name="value"></param>
        public void SysStringTestCreate(string value, string type)
        {
            manager.Navigation.OpenHomePage();
            manager.Navigation.GoToDataTypesMenu();
            manager.Navigation.GoToTypesTable();
            manager.Rdev.ClickAdd();
            //получение id записи
            string recid = manager.Base.GetRecid();

            //проверка что запись несохраненная (recstate=0)
            manager.Base.CheckingRecstate(0, recid);

            FillFieldSysString(value);
            //сохраняем текущий url
            string url = driver.Url;
            manager.Rdev.SubmitChanges();
            
            //проверка, что после сохранения recstate=1
            int recstateAfterSave = manager.Db.CheckRecstateInDb(recid);
            Assert.AreEqual(1, recstateAfterSave, $"Ошибка! У созданной записи recstate={recstateAfterSave}. Ожидалось - recstate=1");            
        }        

        public void SysStringTestEdit(string value, string type)
        {
            string recid = manager.Db.GetRecidForTestingType(type);
            //если в БД нет ни одной записи с тестируемым типом данных - создаем
            if (recid == "00000000-0000-0000-0000-000000000000")
            {                
                SysStringTestCreate(value, type);
                recid = manager.Db.GetRecidForTestingType(type);
            }
            manager.Navigation.OpenHomePage();
            manager.Navigation.GoToDataTypesMenu();
            manager.Navigation.GoToTypesTable();
            //получение url страницы
            string url = driver.Url;
            manager.Rdev.ClickFirstRow();
            manager.Rdev.ClickEdit();
            //получаем url страницы с записью тестируемого типа данных
            string urlType = String.Concat(url, @"/", recid);
            driver.Navigate().GoToUrl(urlType);
            FillFieldSysString(value);
            manager.Rdev.SubmitChanges();
            //проверка, что внесенные изменения корректно сохранились в БД, тут нужно придумать что-то для стринга
            string sysString = manager.Db.GetInfoTypesForTestingType(recid, type);
            Assert.AreEqual(value, sysString, "Сохраненное значение типа sysstring не соответствует ожидаемому значению");            
        }
        /// <summary>
        /// Заполнение поля типа SysString
        /// </summary>
        /// <param name="value"></param>
        public void FillFieldSysString(string value)
        {
            string stepInfo = "Заполнение поля типа данных sysString";
            manager.WaitShowElement(By.Name("sysstring_test"), stepInfo);
            var v = driver.FindElement(By.Name("sysstring_test"));
            v.GetAttribute("value");
            v.Click();
            v.SendKeys(Keys.Shift + Keys.Home + Keys.Delete);
            v.SendKeys(value);
        }

        public string SearchingInUi(string value) // нужно доработать 
        {
            manager.Navigation.OpenHomePage();
            manager.Navigation.GoToDataTypesMenu();
            manager.Navigation.GoToTypesTable();            
            if (value == "Equal")
            {
                string v = "0123456789";
                FillSearchingField(v);
                ClickOnSearchingIcon();
                driver.FindElement(By.XPath("//span[@class='dx-menu-item-text' and contains(text(), 'Равно')]")).Click();
                Thread.Sleep(2000);
                var c = driver.FindElements(By.XPath("//td[@aria-colindex='1']")).Count - 2;
                return c.ToString();
            }            
            if (value == "Not Equal")
            {
                string v = "0123456789";
                FillSearchingField(v);
                ClickOnSearchingIcon();
                driver.FindElement(By.XPath("//span[@class='dx-menu-item-text' and contains(text(), 'Не равно')]")).Click();
                Thread.Sleep(2000);
                var c = driver.FindElements(By.XPath("//td[@aria-colindex='1']")).Count - 2;
                return c.ToString();
            }
            if (value == "Contains")
            {
                string v = "456";
                FillSearchingField(v);
                ClickOnSearchingIcon();
                driver.FindElement(By.XPath("//span[@class='dx-menu-item-text' and contains(text(), 'Содержит')]")).Click();
                Thread.Sleep(2000);
                var c = driver.FindElements(By.XPath("//td[@aria-colindex='1']")).Count - 2;
                return c.ToString();
            }
            if (value == "Not contains")
            {
                string v = "456";
                FillSearchingField(v);
                ClickOnSearchingIcon();
                driver.FindElement(By.XPath("//span[@class='dx-menu-item-text' and contains(text(), 'Не содержит')]")).Click();
                Thread.Sleep(2000);
                var c = driver.FindElements(By.XPath("//td[@aria-colindex='1']")).Count - 2;
                return c.ToString();
            }
            if (value == "Starts with")
            {
                string v = "012";
                FillSearchingField(v);
                ClickOnSearchingIcon();
                driver.FindElement(By.XPath("//span[@class='dx-menu-item-text' and contains(text(), 'Начинается с')]")).Click();
                Thread.Sleep(2000);
                var c = driver.FindElements(By.XPath("//td[@aria-colindex='1']")).Count - 2;
                return c.ToString();
            }
            if (value == "Ends with")
            {
                string v = "789";
                FillSearchingField(v);
                ClickOnSearchingIcon();
                driver.FindElement(By.XPath("//span[@class='dx-menu-item-text' and contains(text(), 'Заканчивается на')]")).Click();
                Thread.Sleep(2000);
                var c = driver.FindElements(By.XPath("//td[@aria-colindex='1']")).Count - 2;
                return c.ToString();
            }
            return ToString();            
        }

        public void FillSearchingField(string v)
        {
            string stepInfo = "Заполнение строки поиска";
            manager.WaitShowElement(By.XPath("//input[@type='text']"), stepInfo);
            var a = driver.FindElements(By.XPath("//input[@type='text']"))[0];
            a.Click();
            a.Clear();
            a.SendKeys(v);
        }

        public void ClickOnSearchingIcon()
        {
            var b = driver.FindElements(By.XPath("//span[@class='dx-menu-item-popout-container']"))[0];
            b.Click();
        }
    }
}
