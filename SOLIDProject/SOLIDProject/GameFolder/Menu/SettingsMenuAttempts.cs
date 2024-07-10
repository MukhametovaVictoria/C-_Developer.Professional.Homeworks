using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SOLIDProject.GameFolder.Constants;

namespace SOLIDProject.GameFolder
{
    /// <summary>
    /// Реализация IMenu. Класс меню настроек попыток игры.
    /// Принимает на вход любую реализацию класса BaseSettings. Таким образом обеспечивается абстракция.
    /// </summary>
    class SettingsMenuAttempts : IMenu
    {
        private readonly Dictionary<string, Invoker> _menu;
        public Dictionary<string, Invoker> MenuEntity { get => _menu; }

        public SettingsMenuAttempts(BaseSettings settings)
        {
            _menu = new Dictionary<string, Invoker>() {
                { Menu.ManualSettings, new Invoker(){ ClassName = $"{settings.CurrentClassName}", 
                                                      MethodName =  "GetNewAttemptsNumberManually",
                                                      ConstructorPrameters = new Dictionary<string, object>(){ { settings.CurrentClassName, settings} } } },
                { Menu.AutoSettings, new Invoker(){ ClassName = $"{settings.CurrentClassName}",
                                                      MethodName =  "GetAutoAttempts",
                                                      ConstructorPrameters = new Dictionary<string, object>(){ { settings.CurrentClassName, settings} }} },
                { Menu.Back, new Invoker(){ } }
            };
        }
    }
}
