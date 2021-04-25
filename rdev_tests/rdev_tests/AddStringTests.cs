using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace rdev_tests
{
    [TestFixture]
    class AddStringTests : StringTestBase
    {
        public static IEnumerable<StringData> RandomGroupDataProvider() //генерит рандомные данные
        {
            List<StringData> strings = new List<StringData>();
            for (int i = 0; i < 5; i++)
            {
                strings.Add(new StringData(GenerateRandomString(30))
                {
                    StringObj = GenerateRandomString(100)
                });
            }
            return strings;
        }

        [Test, TestCaseSource("RandomGroupDataProvider")]
        public void AddStringTest(StringData stringData)
        {
            List<StringData> oldStrings = app.Strings.GetStringsList();

            app.Strings.Create(stringData);

            Assert.AreEqual(oldStrings.Count + 1, app.Strings.GetStringsCount());

            List<StringData> newStrings = app.Strings.GetStringsList();
            oldStrings.Add(stringData);
            oldStrings.Sort();
            newStrings.Sort();
            Assert.AreEqual(oldStrings, newStrings);
        }
        [Test]
        public void TestDBConnectivity()
        {
            List<StringData> fromDB = StringData.GetAll();
            Console.Out.WriteLine(fromDB);                             
        }
    }
}
