using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home4.Tables
{
    [Table(Name = "Lesson")]
    public class Lesson
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }
        [Column]
        public string Name { get; set; }
        
    }
}
