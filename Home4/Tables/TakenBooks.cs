using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home4.Tables
{
    [Table(Name = "TakenBooks")]
    public class TakenBooks
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = false)]
        public int StudentID { get; set; }
        [Column(IsPrimaryKey = true, IsDbGenerated = false)]
        public int BookID { get; set; }
    }
}
