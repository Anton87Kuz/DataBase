using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home4.Tables
{
    [Table(Name = "TakenLessons")]
    public class TakenLessons
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = false)]
        public int LessonID { get; set; }
        [Column(IsPrimaryKey = true, IsDbGenerated = false)]
        public int? StudentID { get; set; }
    }
}
