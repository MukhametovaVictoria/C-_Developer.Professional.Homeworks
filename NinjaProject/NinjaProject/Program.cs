using Entities.AbstractClasses;
using Enums;
using Factories;
using System;
using System.Collections.Generic;

namespace NinjaProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите деревню шиноби:\n");
            foreach(var village in DataClass.Villages)
            {
                Console.WriteLine($"{(int)village.Key}. {village.Value}.");
            }
            var villageNumber = GetNumber();

            var villageEnum = Village.Konohagakure;
            Enum.TryParse<Village>(villageNumber.ToString(), out villageEnum);
            var ninjas = DataClass.Ninja[villageEnum];

            Console.WriteLine("Выберите шиноби:\n");
            for (var i = 0; i < ninjas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ninjas[i]}.");
            }
            var ninjaNumber = GetNumber();

            var mainFactory = new MainNinjaFactory();
            var ninjaFactory = mainFactory.GetNinjaFactory(villageNumber);
            var ninja = ninjaFactory.CreateNinja(ninjaNumber);
            ninja.PrintInfo();

            Console.WriteLine("Сколько клонов создать?\n");
            var clonesNumber = GetNumber();
            Console.WriteLine("А каким способом?\n");
            Console.WriteLine("1.IMyCloneable.");
            Console.WriteLine("2.ICloneable.\n");
            var cloneable = GetNumber();
            var clones = new List<BaseNinjaPrototype>();
            for(var i = 0; i < clonesNumber; i++)
            {
                if(cloneable < 2)
                    clones.Add(ninja.MyClone());
                else
                    clones.Add((BaseNinjaPrototype)ninja.Clone());
            }

            if(clones.Count > 0)
            {
                var numIsNotOk = true;
                Console.WriteLine($"Сейчас у вас есть {clones.Count} клонов. Поменяйте внешность у клона (укажите номер клона).\n");
                var cloneNumber = 0;
                while (numIsNotOk)
                {
                    cloneNumber = GetNumber();
                    if (cloneNumber > 0)
                        cloneNumber -= 1;
                    if (cloneNumber > clones.Count)
                        Console.WriteLine($"Номер клона превышает количество клонов. Введите другое число.\n");
                    else
                        numIsNotOk = false;
                }
                Console.WriteLine($"Введите описание внешности.\n");
                var str = Console.ReadLine();
                clones[cloneNumber].Appearance = str;
            }

            Console.WriteLine($"Описание клонов:\n");
            foreach (var clone in clones)
            {
                clone.PrintInfo();
            }

            Console.ReadKey();
        }

        private static int GetNumber()
        {
            var str = Console.ReadLine();
            var number = 0;
            int.TryParse(str, out number);

            return number;
        }
    }
}
