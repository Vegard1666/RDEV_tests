using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace rdev_tests.Model
{
    [Table(Name = "types_table")]
    public class TypesData
    {
        public TypesData() { }
        public TypesData(string v) { }

        [Column(Name = "recid"), PrimaryKey]
        public Guid Recid { get; set; }

        [Column(Name = "recstate")]
        public int Recstate { get; set; }

        [Column(Name = "sysstring_test")]
        public string Sysstring { get; set; }         
    }
}