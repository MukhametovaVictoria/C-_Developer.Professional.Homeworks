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
    /// Синхронный вызов параллельного считывания файлов. Вариант 2, через Task.Factory.StartNew и Task.WaitAll
    /// </summary>
    public class VariantTwo : IVariant
    {
        public void Start()
        {
            try
            {
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

        public void ReadThreeFiles()
        {
            var files = new string[] { @"..\..\Files\TextFile1.txt", @"..\..\Files\TextFile2.txt", @"..\..\Files\TextFile3.txt" };
            var spaceCounts = GetSpacesInFiles(files);

            Console.WriteLine($"\nРезультат выполнения считывания трех файлов:");
            for (int i = 0; i < files.Length; i++)
                Console.WriteLine($"Количество пробелов в {files[i]}: {spaceCounts[i]}");

            Console.WriteLine();
        }

        public int[] GetSpacesInFiles(IEnumerable<string> files)
        {
            var filesArr = files.ToArray();
            var spaceCounts = new int[filesArr.Length];
            var tasks = new Task[filesArr.Length];

            for (var i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Factory.StartNew((Object obj) =>
                {
                    var num = (int)obj;
                    var filePath = filesArr[num];
                    using (var reader = new StreamReader(filePath))
                    {
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        string content = reader.ReadToEnd();
                        var count = content.Count(c => c == ' ');
                        stopwatch.Stop();
                        Console.WriteLine($"Путь до файла = {filePath}, Количество пробелов = {count}, Поток = {Thread.CurrentThread.ManagedThreadId}.\nВремя считывания и подсчета пробелов: {stopwatch.ElapsedMilliseconds} мс.\n");
                        spaceCounts[num] = count;
                    }
                }, i);
            }

            Task.WaitAll(tasks);

            return spaceCounts;
        }
    }
}
