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
    }
}
