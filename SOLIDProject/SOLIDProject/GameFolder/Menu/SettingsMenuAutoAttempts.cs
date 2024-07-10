using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SOLIDProject.GameFolder.Constants;

namespace SOLIDProject.GameFolder
{
    class SettingsMenuAutoAttempts : IMenu
    {
        private readonly Dictionary<string, Invoker> _menu;
        public Dictionary<string, Invoker> MenuEntity { get => _menu; }

        public SettingsMenuAutoAttempts(BaseSettings settings)
        {
            _menu = new Dictionary<string, Invoker>() {
                { Menu.NewNumberWithMax, new Invoker(){ ClassName = "SOLIDProject.GameFolder.NumberGenerator",
                                                             MethodName =  "GenerateNumberWithMax" } },
                { Menu.NewNumberWithoutMax, new Invoker(){ ClassName = "SOLIDProject.GameFolder.NumberGenerator",
                                                                MethodName =  "GenerateNumber" } },
                { Menu.Back, new Invoker(){ } }
            };
        }
    }
}
