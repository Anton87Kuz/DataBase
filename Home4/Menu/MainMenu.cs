using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home4.Menu
{
    public class MainMenu: IMenu
    {

        
        private void ShowAllMenu(List<IItemOfMenu> itemOfMenus)
        {
            int i = 1;
            foreach (var item in itemOfMenus)
            {
                Console.WriteLine($"{i++}. {item.Name}");
            }
            Console.WriteLine("0. Exit");
        }

        private int GetCurrentChoise(List<IItemOfMenu> itemOfMenus)
        {
            var readKey = Console.ReadLine();
            int result;

            Console.Clear();

            if (int.TryParse(readKey, out result) && itemOfMenus.Count < result && result > 0)
            {
                result = -1;
            }

            return result;
        }

        public void Start(List<IItemOfMenu> itemOfMenus)
        {
            do
            {
                Console.Clear();
                ShowAllMenu(itemOfMenus);
                var currentChoise = GetCurrentChoise(itemOfMenus);
                if (currentChoise == -1)
                {
                    Console.WriteLine("Incorect data");
                    continue;
                }
                if (currentChoise == 0)
                {
                    break;
                }

                itemOfMenus[currentChoise - 1].Run();

            } while (true);
        }

    }
}

