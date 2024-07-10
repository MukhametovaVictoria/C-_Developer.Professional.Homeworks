using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SOLIDProject.GameFolder.Constants;

namespace SOLIDProject.GameFolder
{
    class SettingsMenuAutoRange : IMenu
    {
        private readonly Dictionary<string, Invoker> _menu;
        public Dictionary<string, Invoker> MenuEntity { get => _menu; }

        public SettingsMenuAutoRange(BaseSettings settings)
        {
            _menu = new Dictionary<string, Invoker>() {
                { Menu.NewNumberWithMax, new Invoker(){ ClassName = "SOLIDProject.GameFolder.NumberGenerator",
                                                             MethodName =  "GenerateRangeWithMax" } },
                { Menu.NewNumberWithoutMax, new Invoker(){ ClassName = "SOLIDProject.GameFolder.NumberGenerator",
                                                                MethodName =  "GenerateRange" } },
                { Menu.Back, new Invoker(){ } }
            };
        }
    }
}
