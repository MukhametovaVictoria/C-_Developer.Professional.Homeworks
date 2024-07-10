using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDProject.GameFolder
{
    /// <summary>
    /// Сервис выбора пункта меню. Вызывается первым при запуске программы.
    /// Принимает на вход любую реализацию IMenu
    /// Выдержаны принципы S, L, D:
    /// Занимается только выбором пункта меню
    /// Принимает на вход взаимозаменяемые классы
    /// Придерживается абстракции (нет конкретных классов)
    /// </summary>
    class MenuService
    {
        private readonly IMenu _menu;

        public MenuService(IMenu menu)
        {
            if(CheckMenu(menu))
                _menu = menu;
        }

        public int GetMenuDecision()
        {
            Console.WriteLine(Constants.Menu.MenuHeader);
            for (var i = 0; i < _menu.MenuEntity.Count; i++)
            {
                Console.WriteLine(i + " - " + _menu.MenuEntity.Keys.ElementAt(i));
            }

            var result = Console.ReadLine();
            var decision = -1;

            while (!int.TryParse(result, out decision) || decision >= _menu.MenuEntity.Count || decision < 0)
            {
                Console.WriteLine(Constants.Menu.NotFound);
                result = Console.ReadLine();
            }

            return decision;
        }

        public object GetResultByDecision()
        {
            var invoker = GetInvokerByDecision();

            if (invoker == null)
                return null;

            return invoker.InvokeMethod();
        }

        public Invoker GetInvokerByDecision()
        {
            var decision = GetMenuDecision();

            var invoker = _menu.MenuEntity.Values.ElementAt(decision);

            if (invoker == null || String.IsNullOrEmpty(invoker.MethodName))
                return null;

            return invoker;
        }

        private bool CheckMenu(IMenu menu)
        {
            if (menu == null || menu.MenuEntity == null || menu.MenuEntity.Count == 0)
                throw new Exception("Нет пунктов в меню");

            return true;
        }
    }
}
