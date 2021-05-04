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

        /// <summary>
        /// Тест типа данных SysInt. Добавление записи
        /// </summary>
        /// <param name="value"></param>
        public void SysStringTestCreate(string value, string type)
        {
            manager.Navigation.OpenHomePage();
            manager.Rdev.ClickDataType();
            manager.Rdev.ClickAllTypes();
            manager.Rdev.ClickAdd();
            //получение id записи
            string recid = manager.Base.GetRecid();

            //проверка что запись несохраненная (recstate=0)
            manager.Base.CheckingRecstate(0, recid);

            manager.Rdev.FillFieldSysString(value);
            //сохраняем текущий url
            string url = driver.Url;
            manager.Rdev.SaveСhangesNote();
            int recstateAfterSave = 0;
            //если тест негативный
            if (value == "-21474836480" || value == "21474836470")
            {
                //проверка предупреждающего сообщение о некорректном значении
                ChekingMessage(value);

                //проверка, что пользователь остался на странице создания записи
                Assert.AreEqual(url, driver.Url, "Url не равен ожидаемому значению. Пользователь не остался на странице с записью после попытки сохранить некорректное значение");

                //проверка что поле в котором введено некорректное значение подсвечено красным
                bool colorField = manager.Base.ColorFieldTestingTypeIsRed(type);
                Assert.IsTrue(colorField, "Поле в котором введено некорректное значение подсвечено красным");
                //проверка, что после сохранения recstate=0
                recstateAfterSave = manager.Db.CheckRecstateInDb(recid);
                Assert.AreEqual(0, recstateAfterSave, $"Ошибка! У созданной записи recstate={recstateAfterSave}. Ожидалось - recstate=0");

            }
            else
            {
                //проверка, что после сохранения recstate=1
                recstateAfterSave = manager.Db.CheckRecstateInDb(recid);
                Assert.AreEqual(1, recstateAfterSave, $"Ошибка! У созданной записи recstate={recstateAfterSave}. Ожидалось - recstate=1");

                //проверка, что внесенные изменения корректно сохранились в БД
                string sysInt = manager.Db.GetInfoTypesForTestingType(recid, type);

                if (value == "!@#$%^&*()_+}{|" || value == "Test" || value == "тест" || value == "")
                {
                    Assert.AreEqual("", sysInt, "Сохраненное значение типа sysint не равен ожидаемому значению");
                }
                else
                {
                    Assert.AreEqual(value, sysInt, "Сохраненное значение типа sysint не равен ожидаемому значению");
                }
            }

        }
        /// <summary>
        /// Проверка предупреждающего сообщения при вводе некорректного значения
        /// </summary>
        public void ChekingMessage(string value)
        {
            string stepInfo = "Проверка предупреждающего сообщения при вводе некорректного значения";
            if (value == "-21474836480")
            {
                manager.WaitShowElement(By.XPath("//small[@class='form-text text-muted']"), stepInfo);
                string text = driver.FindElement(By.XPath("//small[@class='form-text text-muted']")).Text;
                Assert.AreEqual("Минимально допустимое значение для данного типа: -2147483648", text, "При вводе значения превышающее допустимое не возникло предупреждающего сообщения");
            }
            else if (value == "21474836470")
            {
                manager.WaitShowElement(By.XPath("//small[@class='form-text text-muted']"), stepInfo);
                string text = driver.FindElement(By.XPath("//small[@class='form-text text-muted']")).Text;
                Assert.AreEqual("Максимально допустимое значение для данного типа: 2147483647", text, "При вводе значения превышающее допустимое не возникло предупреждающего сообщения");
            }
        }

        public void SysIntTestEdit(string value, string type)
        {
            string recid = manager.Db.GetRecidForTestingType(type);
            //если в БД нет ни одной записи с тестируемым типом данных - создаем
            if (recid == "00000000-0000-0000-0000-000000000000")
            {
                SysStringTestCreate(value, type);
                recid = manager.Db.GetRecidForTestingType(type);
            }
            manager.Navigation.OpenHomePage();
            manager.Rdev.ClickDataType();
            manager.Rdev.ClickAllTypes();
            //получение url страницы
            string url = driver.Url;
            manager.Rdev.ClickFirstRow();
            manager.Rdev.ClickEdit();
            //получаем url страницы с записью тестируемого типа данных
            string urlType = String.Concat(url, @"/", recid);
            driver.Navigate().GoToUrl(urlType);
            manager.Rdev.FillFieldSysString(value);
            manager.Rdev.SaveСhangesNote();
            //проверка, что внесенные изменения корректно сохранились в БД
            string sysInt = manager.Db.GetInfoTypesForTestingType(recid, type);
            if (value == "-21.5")
            {
                Assert.AreEqual("-215", sysInt, "Сохраненное значение типа sysint не равен ожидаемому значению");
            }
            else
            {
                Assert.AreEqual(value, sysInt, "Сохраненное значение типа sysint не равен ожидаемому значению");
            }
        }
    }
}
