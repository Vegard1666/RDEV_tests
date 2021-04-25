using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace rdev_tests
{
    [Table(Name = "public.sysstring_test_table")]

    public class StringData : IEquatable<StringData>, IComparable<StringData>
    {
        public StringData()
        {
        }

        public StringData(string stringobj)
        {
            StringObj = stringobj;
        }

        public bool Equals(StringData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return StringObj == other.StringObj;
        }

        public override int GetHashCode()
        {
            return StringObj.GetHashCode();
        }

        public override string ToString()
        {
            return "name=" + StringObj;
        }

        public int CompareTo(StringData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return StringObj.CompareTo(other.StringObj);
        }

        [Column(Name = "sysstring_field1"), NotNull]
        public string StringObj { get; set; }

        [Column(Name = "recid"), PrimaryKey, Identity]
        public string RecId { get; set; }

        public static List<StringData> GetAll()
        {
            using (RdevDB db = new RdevDB())
            {
                return (from s in db.Strings select s).ToList();
            }
        }
    }    
}