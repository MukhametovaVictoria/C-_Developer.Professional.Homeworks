using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;
using static SOLIDProject.GameFolder.Constants;

namespace SOLIDProject.GameFolder
{
    class StatisticsService : ITextWriter
    {
        private readonly string _path = "statistics.txt";
        private readonly Statistics _statistics;

        public string FilePath => _path;

        public StatisticsService()
        {
            _statistics = GetStatistics();
        }

        public void ShowStatistic()
        {
            var fails = String.IsNullOrEmpty(_statistics.FailsAmount) ? "0" : _statistics.FailsAmount;
            var wins = String.IsNullOrEmpty(_statistics.WinsAmount) ? "0" : _statistics.WinsAmount;
            Console.WriteLine();
            Console.WriteLine(Info.Wins, wins);
            Console.WriteLine(Info.Fails, fails);
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{Info.IsWin}  |  {Info.GameDate}  |  {Info.AttemptsAmount}  |  {Info.HiddenNumber}  |  {Info.GameRange}");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            foreach (var data in _statistics.Games)
            {
                var isWin = data.IsWin ? Info.Yes : Info.No;
                var strBuilder = new StringBuilder();
                strBuilder.Append($"{isWin}      |  {data.Date}    |  {data.AttemptsCount}");
                strBuilder.Append(GetEmptySpaces(Info.AttemptsAmount, data.AttemptsCount));
                if(!String.IsNullOrEmpty(data.HiddenNumber))
                    strBuilder.Append($"  |  {data.HiddenNumber}");
                else
                    strBuilder.Append($"  |  ");
                strBuilder.Append(GetEmptySpaces(Info.HiddenNumber, data.HiddenNumber));
                if(data.Range != null && data.Range.Count == 2)
                    strBuilder.Append($"  |  {data.Range[0]} - {data.Range[1]}");
                else
                    strBuilder.Append($"  |");

                Console.WriteLine(strBuilder.ToString());
            }
            Console.WriteLine();
        }

        private string GetEmptySpaces(string str1, string str2)
        {
            int count = (str1 != null && str2 != null) ? (str1.Length - str2.Length) : (str1 != null ? str1.Length : (str2 != null ? str2.Length : 0));
            var strBuilder = new StringBuilder();
            for (var i = 0; i < count; i++)
                strBuilder.Append(" ");

            return strBuilder.ToString();
        }

        public Statistics GetStatistics()
        {
            if (File.Exists(_path))
            {
                // Чтение всего содержимого в одной строке и вывод его на экран
                string str = File.ReadAllText(_path);
                var obj = JsonSerializer.DeserializeFromString<Statistics>(str);
                if (obj != null)
                    return obj;
            }

            return new Statistics() { Games = new List<Statistics.GameData>() };
        }

        public void AddDataToStatistics(bool isWin, Game game)
        {
            _statistics.Games.Insert(0, 
                new Statistics.GameData()
                {
                    AttemptsCount = game.Count.ToString(),
                    Date = DateTime.Now.ToString(),
                    IsWin = isWin,
                    Range = new List<string>() { game.Settings.Range[0].ToString(), game.Settings.Range[1].ToString() },
                    HiddenNumber = game.HiddenNumber.ToString()
                });
            
            var amount = 0;
            if (isWin)
            {
                if (int.TryParse(_statistics.WinsAmount, out amount))
                {
                    _statistics.WinsAmount = (amount + 1).ToString();
                }
                else
                    _statistics.WinsAmount = "1";
            }
            else
            {
                if (int.TryParse(_statistics.FailsAmount, out amount))
                {
                    _statistics.FailsAmount = (amount + 1).ToString();
                }
                else
                    _statistics.FailsAmount = "1";
            }

            var json = JsonSerializer.SerializeToString(_statistics);
            WriteToFile(json);
        }

        public void WriteToFile(string text)
        {
            using (StreamWriter writer = new StreamWriter(_path, false))
            {
                writer.WriteLineAsync(text);
                writer.Close();
            }
        }
    }
}
