using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home4.Tables
{
    [Table(Name = "Book")]
    public class Book
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }
        [Column]
        public string Authors { get; set; }
        [Column]
        public string Title { get; set; }
        [Column]
        public int Pages { get; set; }
        [Column]
        public int Year { get; set; }
        [Column]
        public int Quantity { get; set; }
    }
}
