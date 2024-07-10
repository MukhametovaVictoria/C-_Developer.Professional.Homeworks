using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SOLIDProject.GameFolder.Constants;

namespace SOLIDProject.GameFolder
{
    /// <summary>
    /// Реализация IMenu. Класс Главного меню игры.
    /// Принимает на вход любую реализацию класса BaseSettings. Таким образом обеспечивается абстракция.
    /// </summary>
    class MainMenu : IMenu
    {
        private readonly Dictionary<string, Invoker> _menu;
        public Dictionary<string, Invoker> MenuEntity { get => _menu; }

        public MainMenu(BaseSettings settings)
        {
            _menu = new Dictionary<string, Invoker>() {
                { Menu.Settings, new Invoker(){ClassName = $"{settings.CurrentClassName}",
                                               MethodName =  "UpdateSettings",
                                               ConstructorPrameters = new Dictionary<string, object>(){ { settings.CurrentClassName, settings} } } },
                { Menu.PlayGame, new Invoker(){ClassName = "SOLIDProject.GameFolder.Game",
                                               MethodName =  "PlayGame",
                                               ConstructorPrameters = new Dictionary<string, object>(){ { settings.CurrentClassName, settings} }} },
                { Menu.Statistics, new Invoker(){ ClassName = "SOLIDProject.GameFolder.StatisticsService",
                                                  MethodName = "ShowStatistic"} }
            };
        }
    }
}
