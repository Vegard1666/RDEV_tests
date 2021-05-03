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
        public int Sysstring { get; set; }

        [Column(Name = "sysboolean_test")]
        public bool? Sysboolean { get; set; }

        [Column(Name = "sysdate_test")]
        public DateTime Sysdate { get; set; }

        [Column(Name = "sysnumber_test")]
        public int Sysnumber { get; set; }

        [Column(Name = "sysenum_test")]
        public int? Sysenum { get; set; }

        [Column(Name = "sysint_test")]
        public int? Sysint { get; set; }

    }
}