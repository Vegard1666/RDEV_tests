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
        /// Создание записи типа SysBoolean
        /// </summary>
        //[Test, Order(1), Category("SysBoolean")]
        //public void SysBooleanTestCreate()
        //{
        //    app.SysBoolean.SysBooleanTestCreate();
        //    Assert.IsTrue(app.Rdev.CheckboxIsActive(), "Включенный чекбокс отображается как выключенный");

        //}

        /// <summary>
        /// Редактирование первой записи в поле типа sysboolean
        /// </summary>
        //[Test, Order(2), Category("SysBoolean")]
        //public void SysBooleanTestEdit()
        //{
        //    string type = "sysboolean";
        //    //sysBooleanState - состояние типа данных до редактирования
        //    bool? sysBooleanState = app.SysBoolean.SysBooleanTestEdit(type);
        //    //данные не должны совпадать
        //    Assert.AreNotEqual(sysBooleanState, app.Rdev.CheckboxIsActive(), "После редактирования данные не изменились");
        //}
        /// <summary>
        /// Удаление с отменой первой строки
        /// </summary>
        //[Test, Order(3), Category("SysBoolean")]
        //public void SysBooleanTestCancelDelete()
        //{
        //    string action = "Отмена";
        //    //double - открыть запись двойным кликом, right - открыть запись через ПКМ > Открыть
        //    string click = "double";
        //    string type = "sysboolean";
        //    app.Rdev.TestCancelDeleteOrSubmitDelete(type, action, click);
        //}

        /// <summary>
        /// Удаление первой записи тестируемого типа данных найденного в БД
        ///// </summary>
        //[Test, Order(4), Category("SysBoolean")]
        //public void SysBooleanTestDelete()
        //{
        //    string action = "Удалить";
        //    //double - открыть запись двойным кликом, right - открыть запись через ПКМ > Открыть
        //    string click = "right";
        //    string type = "sysboolean";
        //    app.Rdev.TestCancelDeleteOrSubmitDelete(type, action, click);
        //}
        /// <summary>
        /// Создание записи типа данных SysDate вручную заполняя поля даты
        /// </summary>
        ///// <param name="value"></param>
        //[Test, Order(5), Category("SysDate")]
        //public void SysDateTestCreate(
        //    [Values(
        //    "15.05.2021", "29122019",
        //    "35.07.2021", "30022020",
        //    "02.01.202155", "11.!@#?05тест21")] string value)
        //{
        //    string type = "sysdate";
        //    app.SysDate.SysDateTestCreate(value, type);
        //}
        /// <summary>
        /// Создание записи типа данных SysDate с выбором даты через календарь
        /// </summary>

        /// <summary>
        /// Редактирование записи типа данных SysDate с выбором даты через календарь
        /// </summary>
        //[Test, Order(7), Category("SysDate")]
        //public void SysDateTestEditDateBox()
        //{
        //    string value = null;
        //    string type = "sysdate";
        //    bool dateBox = true;
        //    Tuple<string, string> dates = app.SysDate.SysDateTestEdit(value, type, dateBox);

        //    DateTime dt = app.Base.GetDateForCompare(dates.Item2);
        //    string date = dt.ToString();

        //    Assert.AreEqual(date, dates.Item1, "Сохраненная дата из календаря не равна следующему месяцу от ранее выбранной даты");
        //}
        /// <summary>
        /// Редактирование записи типа данных SysDate вручную заполняя поля даты
        /// </summary>
        ///// <param name="value"></param>
        //[Test, Order(8), Category("SysDate")]
        //public void SysDateTestEdit()
        //{
        //    string value = "01.01.2021";
        //    string type = "sysdate";
        //    bool dateBox = false;
        //    app.SysDate.SysDateTestEdit(value, type, dateBox);
        //}
        /// <summary>
        /// Удаление записи типа данных SysDate с отменой
        /// </summary>
        //[Test, Order(9), Category("SysDate")]
        //public void SysDateTestCancelDelete()
        //{
        //    string action = "Отмена";
        //    //double - открыть запись двойным кликом, right - открыть запись через ПКМ > Открыть
        //    string click = "double";
        //    string type = "sysdate";
        //    app.Rdev.TestCancelDeleteOrSubmitDelete(type, action, click);
        //}
        /// <summary>
        /// Удаление записи типа данных SysDate
        /// </summary>
        //[Test, Order(10), Category("SysDate")]
        //public void SysDateTestDelete()
        //{
        //    string action = "Удалить";
        //    //double - открыть запись двойным кликом, right - открыть запись через ПКМ > Открыть
        //    string click = "right";
        //    string type = "sysdate";
        //    app.Rdev.TestCancelDeleteOrSubmitDelete(type, action, click);
        //}

        /// <summary>
        /// Создание записи типа данных SysDate вручную заполняя поля даты
        /// </summary>
        /// <param name="value"></param>
        [Test, Order(11), Category("SysInt")]
        public void SysIntTestCreate(
            [Values(
            "-2147483648", "2147483647",
            "-123456", "50",
            "-21474836480", "21474836470", "0",
            "", "тест",
            "Test", "!@#$%^&*()_+}{|")] string value)
        {
            string type = "sysint";
            app.SysInt.SysIntTestCreate(value, type);
        }

        /// <summary>
        /// Редактирование записи типа данных SyInt
        /// </summary>
        /// <param name="value"></param>
        [Test, Order(12), Category("SysInt")]
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

        /// <summary>
        /// Создание записи типа данных SysEnum
        /// </summary>
        //[Test, Order(15), Category("SysEnum")]
        //public void SysEnumTestCreate()
        //{
        //    string type = "sysenum";
        //    int value = 1; //value - номер строки из выпадающего списка
        //    app.SysEnum.SysEnumTestCreate(value, type);
        //}

        /// <summary>
        /// Редактирование записи типа данных SysEnum
        /// </summary>
    //    [Test, Order(16), Category("SysEnum")]
    //    public void SysEnumTestEdit()
    //    {
    //        string type = "sysenum";
    //        int value = 2; //value - номер строки из выпадающего списка
    //        app.SysEnum.SysEnumTestEdit(value, type);
    //    }

    //    /// <summary>
    //    /// Проверка, что поле SysEnum может быть null
    //    /// </summary>
    //    [Test, Order(17), Category("SysEnum")]
    //    public void SysEnumTestClearField()
    //    {
    //        string type = "sysenum";
    //        app.SysEnum.SysEnumTestClearField(type);
    //    }

    //    /// <summary>
    //    /// Удаление записи с отменой типа sysEnum
    //    /// </summary>
    //    [Test, Order(18), Category("SysEnum")]
    //    public void SysEnumTestCancelDelete()
    //    {
    //        string action = "Отмена";
    //        //double - открыть запись двойным кликом, right - открыть запись через ПКМ > Открыть
    //        string click = "double";
    //        string type = "sysenum";
    //        app.Rdev.TestCancelDeleteOrSubmitDelete(type, action, click);
    //    }

    //    /// <summary>
    //    /// Удаление записи типа sysEnum
    //    /// </summary>
    //    [Test, Order(19), Category("SysEnum")]
    //    public void SysEnumTestDelete()
    //    {
    //        string action = "Удалить";
    //        //double - открыть запись двойным кликом, right - открыть запись через ПКМ > Открыть
    //        string click = "right";
    //        string type = "sysenum";
    //        app.Rdev.TestCancelDeleteOrSubmitDelete(type, action, click);
    //    }
    }
}