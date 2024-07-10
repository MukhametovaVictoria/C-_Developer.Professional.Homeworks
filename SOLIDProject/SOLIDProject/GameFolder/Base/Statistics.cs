using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDProject.GameFolder
{
    class Statistics
    {
        public string WinsAmount { get; set; }
        public string FailsAmount { get; set; }
        public List<GameData> Games { get; set; }
        public class GameData
        {
            public bool IsWin { get; set; }
            public string AttemptsCount { get; set; }
            public string Date { get; set; }
            public List<string> Range { get; set; }
            public string HiddenNumber { get; set; }
        }
    }
}
