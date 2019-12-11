using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home4.Menu
{
    public class Mainwork
    {
        private readonly IMenu _menu;

        public Mainwork()
        {
            _menu = new MainMenu();
        }

        public void Start()
        {
            
            _menu.Start(PrepareMenu());
        }

        private List<IItemOfMenu> PrepareMenu()
        {
            return new List<IItemOfMenu>() { new ShowBooks(), new ShowLessons(), new GetStudents(), new SetBook(), 
               new SetLesson(), new GetAll()};
        }
    }
}
