using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDProject.GameFolder
{
    /// <summary>
    /// Класс меню игры
    /// Реализация принципа L - Liskov substitution
    /// Обеспечение взаимозаменяемости объектов экземплярами их подтипов без изменения корректности этой программы
    /// Принимаем в качестве параметра IMenu в каком-то методе, а передаем в него экземпляр реализации IMenu
    /// </summary>
    interface IMenu
    {
        /// <summary>
        /// Справочник пунктов меню, где string - пункт меню, 
        /// Invoker - объект, в котором передаются данные о классе и методе для вызова, при выборе данного пункта меню
        /// </summary>
        Dictionary<string, Invoker> MenuEntity { get;}
    }
}
