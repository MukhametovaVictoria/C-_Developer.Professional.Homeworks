using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProject
{
    /// <summary>
    /// Синхронный вызов параллельного считывания файлов. Вариант 1, через Parallel.ForEach
    /// </summary>
    public class VariantOne : IVariant
    {
        public void Start()
        {
            try
            {
                // Прочитать 3 файла параллельно и вычислить количество пробелов
                ReadThreeFiles();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка в процессе выполения: {ex.Message}\n");
            }

            while (true)
            {
                try
                {
                    // Прочитать все файлы в папке и вычислить количество пробелов
                    Console.WriteLine("\nВведите полный путь до папки, из которой считать файлы формата .txt:\n");
                    var folderPath = Console.ReadLine();

                    ReadFilesInFolder(folderPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nОшибка в процессе выполения: {ex.Message}\n");
                }
            }
        }

        /// <summary>
        /// Прочитать 3 файла параллельно и вычислить количество пробелов
        /// </summary>
        public void ReadThreeFiles()
        {
            var files = new string[] { @"..\..\Files\TextFile1.txt", @"..\..\Files\TextFile2.txt", @"..\..\Files\TextFile3.txt" };
            var spaceCounts = GetSpacesInFiles(files);
            Console.WriteLine($"\nРезультат выполнения считывания трех файлов:");
            for (int i = 0; i < files.Length; i++)
                Console.WriteLine($"Количество пробелов в {files[i]}: {spaceCounts[i]}");

            Console.WriteLine();
        }

        /// <summary>
        /// Прочитать все файлы в папке и вычислить количество пробелов
        /// </summary>
        public void ReadFilesInFolder(string folderPath)
        {
            if (String.IsNullOrEmpty(folderPath))
            {
                Console.WriteLine("\nКаталога не существует.\n");
                return;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
            if (!dirInfo.Exists)
            {
                Console.WriteLine("\nКаталога не существует.\n");
                return;
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            string[] filesInFolder = Directory.GetFiles(folderPath, "*.txt");
            if (filesInFolder.Length == 0)
            {
                Console.WriteLine("\nНет файлов в каталоге.\n");
                stopwatch.Stop();
                return;
            }

            var spaceCounts = GetSpacesInFiles(filesInFolder);
            var totalSpaceCount = spaceCounts.Sum();
            stopwatch.Stop();
            Console.WriteLine($"\nРезультат выполнения считывания файлов из папки {folderPath}:");
            Console.WriteLine($"Общее количество файлов в папке: {filesInFolder.Length}");
            Console.WriteLine($"Общее количество пробелов в папке: {totalSpaceCount}");
            Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс.\n");
        }

        /// <summary>
        /// Параллельное выполнение чтения файлов и подсчета количества пробелов
        /// </summary>
        /// <param name="files">Список файлов</param>
        /// <returns>Массив количества пробелов в каждом файле</returns>
        public int[] GetSpacesInFiles(IEnumerable<string> files)
        {
            var result = new List<int>();
            Parallel.ForEach(files, (filePath) =>
            {
                using (var reader = new StreamReader(filePath))
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    string content = reader.ReadToEnd();
                    var count = content.Count(c => c == ' ');
                    stopwatch.Stop();
                    Console.WriteLine($"Путь до файла = {filePath}, Количество пробелов = {count}, Поток = {Thread.CurrentThread.ManagedThreadId}.\nВремя считывания и подсчета пробелов: {stopwatch.ElapsedMilliseconds} мс.\n");
                    result.Add(count);
                }
            });

            return result.ToArray();
        }
    }
}
