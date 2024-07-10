using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SOLIDProject.GameFolder.Constants;

namespace SOLIDProject.GameFolder
{
    /// <summary>
    /// Генерация чисел
    /// Реализация принципа I - Interface segregation
    /// Наследуемся от интерфейса IGenerator и реализовываем метод GenerateNumber по своему, в зависимости от смысла класса
    /// Дополнительно добавлен метод генерации диапазона
    /// </summary>
    class NumberGenerator : IGenerator
    {
        public int[] GenerateRange()
        {
            Random random = new Random();
            var num1 = random.Next();
            var num2 = random.Next();
            while (num1 == num2)
            {
                num2 = random.Next();
            }

            if (num1 < num2)
                return new int[2] { num1, num2 };

            return new int[2] { num2, num1 };
        }

        public int[] GenerateRangeWithMax()
        {
            var number = GetMaxNumber();

            Random random = new Random();
            var num1 = random.Next(number);
            var num2 = random.Next(number);
            while (num1 == num2)
            {
                num2 = random.Next(number);
            }

            if (num1 < num2)
                return new int[2] { num1, num2 };

            return new int[2] { num2, num1 };
        }

        public int GenerateNumber()
        {
            Random random = new Random();
            return random.Next();
        }

        public int GenerateNumberWithMax()
        {
            var number = GetMaxNumber();

            Random random = new Random();
            return random.Next(number);
        }

        private int GetMaxNumber()
        {
            Console.WriteLine(Info.EnterMaxNumber);
            var num = Console.ReadLine();
            int number = 0;
            while (!int.TryParse(num, out number))
            {
                Console.WriteLine(Info.InvalidNumber);
                num = Console.ReadLine();
            }

            return number;
        }
    }
}
