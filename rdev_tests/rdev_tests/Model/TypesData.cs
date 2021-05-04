using System;
using LinqToDB.Mapping;

namespace rdev_tests.Model
{
    [Table(Name = "types_table")]
    public class TypesData
    {        
        [Column(Name = "recid"), PrimaryKey]
        public Guid Recid { get; set; }

        [Column(Name = "recstate")]
        public int Recstate { get; set; }

        [Column(Name = "sysstring_test")]
        public string Sysstring { get; set; }         
    }
}