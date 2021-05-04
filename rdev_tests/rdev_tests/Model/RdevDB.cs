using LinqToDB;
using LinqToDB.Data;

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
