using System.IO;

namespace DelegatesProj
{
    static class ConverterToFloat
    {
        /// <summary>
        /// Конвертация FileInfo в float
        /// </summary>
        /// <param name="file">Файл</param>
        /// <returns>Размер файла + длина строки названия</returns>
        public static float ConvertFileInfoToFloat(FileInfo file)
        {
            if (file == null)
                return 0;

            var result = file.FullName.Length + file.Length;
            return (float)result;
        }

        /// <summary>
        /// Конвертация строки в float
        /// </summary>
        /// <param name="str">Строка</param>
        /// <returns>Длина строки в float</returns>
        public static float ConvertStringToFloat(string str)
        {
            if (str == null)
                return 0;

            return (float)str.Length;
        }
    }
}
