using Newtonsoft.Json;
using NUnit.Framework;
using rdev_tests.Settings;
using System;
using System.IO;
using System.Text;


namespace rdev_tests.Tests
{
    public class TestBase
    {
        protected ApplicationManager app;

        [SetUp]
        public void SetupApplicationManager()
        {
            var json = File.ReadAllText("settings.json");
            var settings = JsonConvert.DeserializeObject<SettingsJson>(json);

            app = ApplicationManager.GetInstance(settings);
        }

        [OneTimeTearDown]
        public void AfterTest()
        {
            if (app.Driver != null) app.Driver.Quit();
        }

        public static Random rnd = new Random();
        private static int number;

        public static string GenerateRandomString(int length)
        {
            //int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append((char)rnd.Next(0x0430, 0x44F));
            }
            return builder.ToString();
        }
        public static string GenerateRandomNum(int length)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                number = rnd.Next(1, 9);
                string num = number.ToString();
                builder.Append(num);
            }
            //string num = number.ToString();
            return builder.ToString();
        }
    }
}
