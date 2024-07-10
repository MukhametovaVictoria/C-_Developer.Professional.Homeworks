using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDProject.GameFolder
{
    /// <summary>
    /// Генерация чисел
    /// Реализация принципа I - Interface segregation
    /// Наследуемся от данного интерфейса и реализовываем метод GenerateNumber по своему, в зависимости от смысла класса
    /// </summary>
    interface IGenerator
    {
        int GenerateNumber();
    }
}
