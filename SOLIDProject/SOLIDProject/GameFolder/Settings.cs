using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SOLIDProject.GameFolder.Constants;

namespace SOLIDProject.GameFolder
{
    /// <summary>
    /// Конкретная реализация класса настроек
    /// </summary>
    class Settings : BaseSettings
    {
        public Settings() : base() {}

        public Settings(Settings settings) : base(settings) {}

        /// <summary>
        /// Обновляем настройки, возвращаем итоговый объект
        /// </summary>
        /// <returns></returns>
        public override BaseSettings UpdateSettings()
        {
            var result = new MenuService(new SettingsMenuMain(this)).GetResultByDecision();

            if (result != null)
            {
                if(result is System.Int32[])
                {
                    Range = (int[])result;
                    return this;
                }
                else if(result is System.Int32)
                {
                    if((int)result > 0)
                    {
                        Attempts = (int)result;
                        return this;
                    } 
                }
                else if(result is Settings)
                {
                    return (Settings)result;
                }
            }

            return null;
        }

        public int[] GetNewRange()
        {
            var result = new MenuService(new SettingsMenuRange(this)).GetResultByDecision();

            return result != null ? (int[])result : null;
        }

        public int[] GetAutoRange()
        {
            Console.WriteLine(Menu.AutoRangeMenuHeader);
            var result = new MenuService(new SettingsMenuAutoRange(this)).GetResultByDecision();

            return result != null ? (int[])result : null;
        }

        public int GetAutoAttempts()
        {
            Console.WriteLine(Menu.AutoAttemptsMenuHeader);
            var result = new MenuService(new SettingsMenuAutoAttempts(this)).GetResultByDecision();

            return result != null ? (int)result : 0;
        }

        public int GetNewAttemptsNumber()
        {
            var result = new MenuService(new SettingsMenuAttempts(this)).GetResultByDecision();

            return result != null ? (int)result : 0;
        }

        public int[] GetRangeManually()
        {
            Console.WriteLine(Info.CurrentRange, Range[0], Range[1]);
            Console.WriteLine(Info.EnterFirst);
            var num = Console.ReadLine();
            int number1 = 0;
            int number2 = 0;
            while (!int.TryParse(num, out number1))
            {
                Console.WriteLine(Info.InvalidNumber);
                num = Console.ReadLine();
            }

            Console.WriteLine(Info.EnterSecond);
            num = Console.ReadLine();
            while (!int.TryParse(num, out number2) || number1 == number2)
            {
                Console.WriteLine(Info.InvalidNumberOrTheSame);
                num = Console.ReadLine();
            }

            Console.WriteLine(Info.SettingsSaved);
            if (number1 < number2)
                return new int[2] { number1, number2 };

            return new int[2] { number2, number1 };
        }

        public int GetNewAttemptsNumberManually()
        {
            Console.WriteLine(Info.CurrentAttemptsAmount, Attempts);
            Console.WriteLine(Info.EnterNewNumber);
            var num = Console.ReadLine();
            int number = 0;
            while (!int.TryParse(num, out number) || number <= 0)
            {
                Console.WriteLine(Info.InvalidNumberOrZero);
                num = Console.ReadLine();
            }
            Console.WriteLine(Info.SettingsSaved);
            return number;
        }
    }
}
