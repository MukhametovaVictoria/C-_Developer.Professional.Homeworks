using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDProject.GameFolder
{
    static class Constants
    {
        public static readonly int Attempts = 5;
        public static readonly int[] Range = new int[2] { 0, 20 };

        public static class Menu
        {
            public static readonly string MenuHeader = "Выберите пункт меню (ввод числа): ";
            public static readonly string ManualSettings = "Задать настройки вручную.";
            public static readonly string AutoSettings = "Задать рандомные настройки.";
            public static readonly string ReturnDefault = "Вернуть настройки по умолчанию.";
            public static readonly string Back = "Назад.";
            public static readonly string RangeSettings = "Задать настройки диапазона.";
            public static readonly string AttemptsSettings = "Задать количество попыток.";
            public static readonly string NotFound = "Выбранный пункт не распознан. Повторите ввод.";
            public static readonly string Settings = "Настройки игры.";
            public static readonly string PlayGame = "Играть!";
            public static readonly string Statistics = "Статистика.";
            public static readonly string AutoRangeMenuHeader = "Без установки максимального числа для диапазона загаданное число может состоять из 9 цифр.";
            public static readonly string AutoAttemptsMenuHeader = "Без установки ограничений число количества попыток может состоять из 9 цифр.";
            public static readonly string NewNumberWithMax = "Установить максимальное число для автоматического выбора.";
            public static readonly string NewNumberWithoutMax = "Установить без ограничений.";
        }

        public static class Info
        {
            public static readonly string EnterNewNumber = "Введите новое число: ";
            public static readonly string EnterMaxNumber = "Введите максимальное число: ";
            public static readonly string SettingsSaved = "Настройки сохранены.";
            public static readonly string InvalidNumber = "Число не распознано. Повторите ввод.";
            public static readonly string InvalidNumberOrTheSame = "Число не распознано или вы ввели одинаковые числа. Повторите ввод.";
            public static readonly string InvalidNumberOrZero = "Число не распознано или оно <= 0. Повторите ввод.";
            public static readonly string EnterFirst = "Введите первое число: ";
            public static readonly string EnterSecond = "Введите второе число: ";
            public static readonly string NewGame = "Нажмите Enter для начала новой игры.";
            public static readonly string FinishAttempts = "У вас закончились попытки.";
            public static readonly string EnterYourNumber = "Ваш вариант: ";
            public static readonly string IntervalInfo = "Угадайте число в интервале от {0} до {1}.\n";
            public static readonly string NumberIsGreater = "Задуманное число больше.";
            public static readonly string NumberIsLess = "Задуманное число меньше.";
            public static readonly string YouWin = "Вы угадали с {0} попытки.";
            public static readonly string CurrentAttemptsAmount = "Текущее количество попыток {0}.\n";
            public static readonly string CurrentRange = "Текущий диапазон {0} - {1}.";
            public static readonly string AttemptsAmount = "Затраченное кол-во попыток";
            public static readonly string HiddenNumber = "Загаданное число";
            public static readonly string IsWin = "Выигрыш";
            public static readonly string GameDate = "Дата игры            ";
            public static readonly string GameRange = "Диапазон";
            public static readonly string Wins = "Победы: {0}";
            public static readonly string Fails = "Поражения: {0}";
            public static readonly string Yes = "Да ";
            public static readonly string No = "Нет";
        }
    }
}
