using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home4.Menu
{
    public interface IItemOfMenu
    {

        string Name { get; }
        void Run();
    }
}
