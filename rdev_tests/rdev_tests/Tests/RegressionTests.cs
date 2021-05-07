using NUnit.Framework;
using rdev_tests.Tests;
using System;
using rdev_tests.AppManager;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;


namespace Tests
{
    public class Tests : TestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Создание записи типа данных SysString c рандомными значениями.
        /// <param name="value"></param>
        [Test, Order(0), Category("SysString")]
        public void RandomSysStringTestCreate()            
        {
            int l = 0;
            string type = "sysstring";
            do
            {
                string value = app.SysString.GenerateRandomString(60);
                app.SysString.SysStringTestCreate(value, type);
                l++;
            } while (l < 5);
        }
        /// <summary>
        /// Создание записи типа данных SysString
        /// <param name="value"></param>
        [Test, Order(1), Category("SysString")]
        public void SysStringTestCreate(
            [Values(
            "", "   ",
            " Пробел до текста, внутри текста и после ", "ТЕКСТВВЕРХНЕМРЕГИСТРЕ",
            "текствнижнемрегистре", "EnglishString", "0123456789",
            "?!,.@;'", "-1234567890",
            "$%^&*}{[]()+|@£€/¶§©®", "Эта строка содержит 255 символов Эта строка содержит 255 символов Эта строка содержит 255 символов Эта строка содержит 255 символов Эта строка содержит 255 символов Эта строка содержит 255 символов Эта строка содержит 255 символов!!!???!!!!!!!!!!!!!!!!!!")] string value)
        {
            string type = "sysstring";
            app.SysString.SysStringTestCreate(value, type);
        }

        /// <summary>
        /// Редактирование записи типа данных SysString
        /// </summary>
        /// <param name="value"></param>
        [Test, Order(2), Category("SysString")]
        public void SysStringTestEdit(
            [Values(
            "Запись изменена!!!", "Row was changed!!!")] string value)
        {
            string type = "sysstring";
            app.SysString.SysStringTestEdit(value, type);
        }
        /// <summary>
        /// Отмена удаления записи с типом данных SysString
        /// </summary>
        [Test, Order(3), Category("SysString")]
        public void SysStringTestCancelDelete()
        {
            string action = "Отмена";
            //double - открыть запись двойным кликом, right - открыть запись через ПКМ > Открыть
            string click = "double";
            string type = "sysstring";
            app.Rdev.TestCancelDeleteOrSubmitDelete(type, action, click);
        }
        /// <summary>
        /// Удаление записи типа данных SysString
        /// </summary>
        [Test, Order(4), Category("SysString")]
        public void SysStringTestDelete()
        {
            string action = "Удалить";
            //double - открыть запись двойным кликом, right - открыть запись через ПКМ > Открыть
            string click = "right";
            string type = "sysstring";
            app.Rdev.TestCancelDeleteOrSubmitDelete(type, action, click);
        }

        [Test, Order(5), Category("SysString")]
        // нужно доработать
        public void SearchingSysString([Values("Equal", "Not Equal", "Contains", "Starts with", "Ends with", "Not contains")] string value)
        {
            string countFromUi = app.SysString.SearchingInUi(value);
            string countFromDb = app.Db.SearchingInDb(value);
            Assert.AreEqual(countFromDb, countFromUi);
        }
    }
}