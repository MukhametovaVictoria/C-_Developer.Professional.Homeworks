using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SOLIDProject.GameFolder.Constants;

namespace SOLIDProject.GameFolder
{
    /// <summary>
    /// Класс игры
    /// Принимает на вход любую реализацию BaseSettings
    /// </summary>
    class Game
    {
        private readonly int _hiddenNumber;
        private readonly BaseSettings _settings;
        private readonly StatisticsService _statisticsService = new StatisticsService();
        private int _count;
        public BaseSettings Settings => _settings;
        public int Count => _count;
        public int HiddenNumber => _hiddenNumber;

        public Game(BaseSettings settings)
        {
            _hiddenNumber = new HiddenNumberGenerator(settings).GenerateNumber();
            _settings = settings;
            _count = 0;
        }

        public void PlayGame()
        {
            Console.WriteLine(Info.IntervalInfo, _settings.Range[0], _settings.Range[1]);
            Console.WriteLine(Info.CurrentAttemptsAmount, _settings.Attempts);
            int number;

            do
            {
                _count++;
                number = GetNumber();
                Console.WriteLine(CompareNumbers(number));
            }
            while (number != _hiddenNumber && Count < _settings.Attempts);

            if (number != _hiddenNumber && Count == _settings.Attempts)
            {
                Console.WriteLine(Info.FinishAttempts);
                _statisticsService.AddDataToStatistics(false, this);
            }

            Console.WriteLine(Info.NewGame);
            Console.ReadLine();
        }

        private string CompareNumbers(int number)
        {
            if (number < _hiddenNumber)
                return Info.NumberIsGreater;
            else if (number > _hiddenNumber)
                return Info.NumberIsLess;

            _statisticsService.AddDataToStatistics(true, this);
            return String.Format(Info.YouWin, Count);
        }

        private int GetNumber()
        {
            string str;
            do
            {
                Console.Write(Info.EnterYourNumber);
                str = Console.ReadLine();
            }
            while (!CheckIsAppropriateNumber(str));
            
            return int.Parse(str);
        }

        private bool CheckIsAppropriateNumber(string number)
        {
            if (int.TryParse(number, out int result))
                return true;

            Console.Write(Info.InvalidNumber);
            return false;
        }
    }
}
