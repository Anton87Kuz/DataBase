using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home4.Menu
{
    public class GetStudents:IItemOfMenu
    {
        public string Name { get { return "Show all students"; } }

        public void Run()
        {
            using (MyData data = new MyData())
            {

                data.GetStudents();
                Console.ReadKey();
            }
        }
    }
}
