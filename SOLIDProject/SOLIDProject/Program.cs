using SOLIDProject.GameFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDProject
{
    class Program
    {
        private static ITextWriter Logger = new LoggerService("logfile.txt");

        /// <summary>
        /// Краткое описание реализации:
        /// 1. У нас есть классы меню, где указано соотношение "Пункт меню" - "Метод для вызова при выборе этого пункта" (подробнее п.4).
        /// 2. Класс MenuService выполняет переходы по пунктам меню, в завимости от того, какую реализацию IMenu в него передать.
        /// 3. Переходы осуществляются с помощью класса Invoker, который вызывает переданные в него методы.
        /// 4. Реализация IMenu принимает на вход BaseSettings, а точнее любую конкретную реализацию BaseSettings. В данном случае это класс Settings.
        ///    IMenu - это что-то вроде объекта справочника. Он хранит отношение "Пункт меню" - "Метод для вызова при выборе этого пункта".
        /// Итого: можно создать любой класс от BaseSettings с кастомными настройками, затем создать любой класс от IMenu и все это передать в MenuService.
        /// MenuService может что-то вернуть одним из своих методов, 
        /// например GetResultByDecision() - получаем результат выполнения метода выбранного пункта меню. Дальше уже решаем что с этим результатом делать.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var settings = new Settings();
            while (true)
            {
                try
                {
                    //Тут мы получаем результат выбора какого-то пункта меню. Если в качестве результата вернулся объект Settings, 
                    //значит пользователь сделал настройки игры и теперь нужно их сохранить. Сохраняем в переменную settings.
                    //Решение о том, какие методы будут выполняться и результаты возвращаться, указывается в MainMenu (реализация IMenu)
                    var result = new MenuService(new MainMenu(settings)).GetResultByDecision();

                    if (result != null && result is Settings)
                    {
                        settings = (Settings)result;
                    }
                }
                catch(Exception ex)
                {
                    Logger.WriteToFile(ex.Message);
                }
            }
        }
    }
}
