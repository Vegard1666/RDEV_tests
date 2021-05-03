using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB.Data;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.DataProvider;
using LinqToDB.Configuration;

namespace rdev_tests.Model
{
    public class RdevDB : DataConnection
    {
        public RdevDB(string connectionString) : base(ProviderName.PostgreSQL, connectionString)
        {
        }

        public ITable<TypesData> Types { get { return GetTable<TypesData>(); } }
    }
}
