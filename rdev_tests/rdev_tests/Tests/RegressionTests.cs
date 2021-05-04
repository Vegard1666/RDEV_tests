using NUnit.Framework;
using rdev_tests.Tests;
using System;

namespace Tests
{
    public class Tests : TestBase
    {
        [SetUp]
        public void Setup()
        {
        }
        
        /// <summary>
        /// Создание записи типа данных SysDate вручную заполняя поля даты
        /// </summary>
        /// <param name="value"></param>
        [Test, Order(11), Category("SysString")]
        public void SysStringTestCreate(
            [Values(
            "-2147483648", "2147483647",
            "-123456", "50",
            "-21474836480", "21474836470", "0",
            "", "тест",
            "Test", "!@#$%^&*()_+}{|")] string value)
        {
            string type = "sysstring";
            app.SysInt.SysStringTestCreate(value, type);
        }

        /// <summary>
        /// Редактирование записи типа данных SysString
        /// </summary>
        /// <param name="value"></param>
        [Test, Order(12), Category("SysString")]
        public void SysIntTestEdit(
            [Values(
            "-21.5", "555")] string value)
        {
            string type = "sysint";
            app.SysInt.SysIntTestEdit(value, type);
        }
        /// <summary>
        /// Удаление записи типа данных SysInt с отменой
        /// </summary>
        [Test, Order(13), Category("SysInt")]
        public void SysIntTestCancelDelete()
        {
            string action = "Отмена";
            //double - открыть запись двойным кликом, right - открыть запись через ПКМ > Открыть
            string click = "double";
            string type = "sysint";
            app.Rdev.TestCancelDeleteOrSubmitDelete(type, action, click);
        }
        /// <summary>
        /// Удаление записи типа данных SysInt
        /// </summary>
        [Test, Order(14), Category("SysInt")]
        public void SysIntTestDelete()
        {
            string action = "Удалить";
            //double - открыть запись двойным кликом, right - открыть запись через ПКМ > Открыть
            string click = "right";
            string type = "sysint";
            app.Rdev.TestCancelDeleteOrSubmitDelete(type, action, click);
        }        
    }
}