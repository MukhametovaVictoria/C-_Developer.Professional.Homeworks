using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Прочитать 3 файла параллельно и вычислить количество пробелов
                ReadThreeFiles();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"\nОшибка в процессе выполения: {ex.Message}\n");
            }

            while (true)
            {
                try
                {
                    Console.ReadKey();

                    // Прочитать все файлы в папке и вычислить количество пробелов
                    Console.WriteLine("\nВведите полный путь до папки, из которой считать файлы формата .txt:\n");
                    var folderPath = Console.ReadLine();
                    if (String.IsNullOrEmpty(folderPath))
                    {
                        folderPath = @"..\..\Files";
                        Console.WriteLine($"\nПуть по умолчанию: {folderPath}\n");
                    }

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
        private static async void ReadThreeFiles()
        {
            var files = new string[] { @"..\..\Files\TextFile1.txt", @"..\..\Files\TextFile2.txt", @"..\..\Files\TextFile3.txt" };
            var spaceCounts = await GetSpacesInFiles(files);
            Console.WriteLine($"\nРезультат выполнения считывания трех файлов:");
            for (int i = 0; i < files.Length; i++)
                Console.WriteLine($"Количество пробелов в {files[i]}: {spaceCounts[i]}");

            Console.WriteLine();
        }

        /// <summary>
        /// Прочитать все файлы в папке и вычислить количество пробелов
        /// </summary>
        private static async void ReadFilesInFolder(string folderPath)
        {
            if(String.IsNullOrEmpty(folderPath))
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

            var spaceCounts = await GetSpacesInFiles(filesInFolder);
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
        private static async Task<int[]> GetSpacesInFiles(IEnumerable<string> files)
        {
            return await Task.WhenAll(files.Select(file => GetSpacesInFile(file)));
        }

        /// <summary>
        /// Подсчет количества пробелов в одном файле
        /// </summary>
        /// <param name="filePath">Путь до файла</param>
        /// <returns>Количество пробелов в файле</returns>
        private static async Task<int> GetSpacesInFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                string content = await reader.ReadToEndAsync();
                var count = content.Count(c => c == ' ');
                Console.WriteLine($"Путь до файла = {filePath}, Количество пробелов = {count}, Поток = {Thread.CurrentThread.ManagedThreadId}.");
                return count;
            }
        }
    }
}
