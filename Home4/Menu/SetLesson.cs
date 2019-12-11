using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home4.Menu
{
    public class SetLesson : IItemOfMenu
    {
        public string Name { get { return "Set lesson to student"; } }

        public void Run()
        {
            using (MyData data = new MyData())
            {

                data.SetLessonToStudent();
                Console.ReadKey();
            }
        }
    }
}
