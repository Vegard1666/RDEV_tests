using System;
using System.Collections.Generic;
using System.Text;
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
            manager.Rdev.ClickDataTypes();
            manager.Rdev.ClickAllTypes();
            manager.Rdev.ClickAdd();
            //получение id записи
            string recid = manager.Base.GetRecid();

            //проверка что запись несохраненная (recstate=0)
            manager.Base.CheckingRecstate(0, recid);

            manager.Rdev.FillFieldSysString(value);
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
            manager.Rdev.ClickDataTypes();
            manager.Rdev.ClickAllTypes();
            //получение url страницы
            string url = driver.Url;
            manager.Rdev.ClickFirstRow();
            manager.Rdev.ClickEdit();
            //получаем url страницы с записью тестируемого типа данных
            string urlType = String.Concat(url, @"/", recid);
            driver.Navigate().GoToUrl(urlType);
            manager.Rdev.FillFieldSysString(value);
            manager.Rdev.SubmitChanges();
            //проверка, что внесенные изменения корректно сохранились в БД, тут нужно придумать что-то для стринга
            string sysString = manager.Db.GetInfoTypesForTestingType(recid, type);
            Assert.AreEqual(value, sysString, "Сохраненное значение типа sysstring не соответствует ожидаемому значению");            
        }
    }
}
