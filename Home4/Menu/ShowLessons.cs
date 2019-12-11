using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home4.Menu
{
    class ShowLessons:IItemOfMenu
    {
        public string Name { get { return "Show all lessons"; } }

        public void Run()
        {
            using (MyData data = new MyData())
            {

                data.GetTakenLessons();
                Console.ReadKey();
            }
        }
    }
}
