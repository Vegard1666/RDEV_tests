using System;
using System.Linq;
using LinqToDB;
using LinqToDB.Data;

namespace rdev_tests
{
    class RdevDB : DataConnection
    {
        public RdevDB() : base(ProviderName.PostgreSQL, @"Server=localhost;Port=5432;Database=rdev2019;User Id=postgres;Password=postgres;")
        {
        }
        public ITable<StringData> Strings { get { return GetTable<StringData>(); } }
    }    
}