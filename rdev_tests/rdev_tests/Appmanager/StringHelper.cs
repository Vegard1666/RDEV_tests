using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace rdev_tests
{
    public class StringHelper : HelperBase
    {
        public StringHelper(AppManager manager) : base(manager)
        {
        }

        private List<StringData> stringCashe = null;

        public List<StringData> GetStringsList()
        {
            if (stringCashe == null)
            {
                stringCashe = new List<StringData>();
                manager.Navigator.GoToStringsTable();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("td.keepMultipleSpaces"));
                // Нужно доработать (не понятно как вытянуть нужную ячейу)
                foreach (IWebElement element in elements)
                {
                    stringCashe.Add(new StringData(null)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }

                string allGroupNames = driver.FindElement(By.CssSelector("div#content form")).Text;
                string[] parts = allGroupNames.Split('\n');
                int shift = groupCashe.Count - parts.Length;
                for (int i = 0; i < groupCashe.Count; i++)
                {
                    if (i < shift)
                    {
                        groupCashe[i].Name = "";
                    }
                    else
                        groupCashe[i].Name = parts[i - shift].Trim();
                }
            }
            return new List<GroupData>(groupCashe);
        }
    }
}
