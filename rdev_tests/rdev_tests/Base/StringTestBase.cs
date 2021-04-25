using System.Collections.Generic;
using NUnit.Framework;

namespace rdev_tests
{
    public class StringTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareGroupsUI_DB()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<StringData> fromUI = app.Strings.GetStringsList();
                List<StringData> fromDB = StringData.GetAll();
                fromUI.Sort();
                fromDB.Sort();
                Assert.AreEqual(fromUI, fromDB);
            }
        }
    }
}
