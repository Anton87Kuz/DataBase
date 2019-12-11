using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home4.Menu
{
    public class ShowBooks : IItemOfMenu
    {
        public string Name { get { return "Show all books"; } }

        public void Run()
        {
            using (MyData data = new MyData())
            {

                data.GetBooks();
                Console.ReadKey();
            }
        }
    }
        
    
}
