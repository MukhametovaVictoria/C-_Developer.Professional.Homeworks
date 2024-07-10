using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SOLIDProject.GameFolder.Constants;

namespace SOLIDProject.GameFolder
{
    /// <summary>
    /// Реализация IMenu. Класс меню настроек игры.
    /// Принимает на вход любую реализацию класса BaseSettings. Таким образом обеспечивается абстракция.
    /// </summary>
    class SettingsMenuMain : IMenu
    {
        private readonly Dictionary<string, Invoker> _menu;
        public Dictionary<string, Invoker> MenuEntity { get => _menu; }

        public SettingsMenuMain(BaseSettings settings)
        {
            _menu = new Dictionary<string, Invoker>() {
                { Menu.RangeSettings, new Invoker(){    ClassName = $"{settings.CurrentClassName}", 
                                                        MethodName =  "GetNewRange", 
                                                        ConstructorPrameters = new Dictionary<string, object>(){ { settings.CurrentClassName, settings} } } },
                { Menu.AttemptsSettings, new Invoker(){ ClassName = $"{settings.CurrentClassName}",
                                                        MethodName =  "GetNewAttemptsNumber",
                                                        ConstructorPrameters = new Dictionary<string, object>(){ { settings.CurrentClassName, settings} } } },
                { Menu.ReturnDefault, new Invoker(){ ClassName = $"{settings.CurrentClassName}",
                                                        MethodName =  "ReturnBaseSettings",
                                                        ConstructorPrameters = new Dictionary<string, object>(){ { settings.CurrentClassName, settings} } } },
                { Menu.Back, new Invoker(){ } }
            };
        }
    }
}
