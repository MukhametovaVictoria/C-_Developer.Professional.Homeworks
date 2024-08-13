using System;
using System.Collections.Generic;

namespace DelegatesProj
{
    public static class ExtensionMethod
    {
        /// <summary>
        /// Метод расширения функциональности коллекций
        /// </summary>
        /// <typeparam name="T">Класс</typeparam>
        /// <param name="collection">Коллекция</param>
        /// <param name="convertToNumber">Функция конвертации данных в тип float</param>
        /// <returns>Объект, выбранный максимальным в передаваемой функции (convertToNumber)</returns>
        public static T GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
        {
            var max = 0.0;
            T elem = default(T);
            foreach (var item in collection)
            {
                var num = convertToNumber(item);
                if (num > max)
                {
                    max = num;
                    elem = item;
                }
            }

            return elem;
        }
    }
}
