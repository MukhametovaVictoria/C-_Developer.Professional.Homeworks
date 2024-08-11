using System;
using System.IO;

namespace DelegatesProj
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new FileSearcher();
            while (true)
            {
                Console.WriteLine("Введите полный путь до директории. Например C:\\, либо нажмите Enter, чтобы оставить текущую директорию.");
                var dirPath = Console.ReadLine();
                Console.WriteLine("Введите шаблон поиска файлов. Например *.jpg или Picture.*, либо нажмите Enter.");
                var pattern = Console.ReadLine();
                Console.WriteLine("Расширение, на котором завершить обработку. Например .txt, либо нажмите Enter, если таковые не нужны.");
                var ext = Console.ReadLine();

                if (String.IsNullOrEmpty(dirPath))
                    dirPath = Directory.GetCurrentDirectory();

                if (String.IsNullOrEmpty(pattern))
                    pattern = "*";
                
                Console.WriteLine();

                test.RunProcess(dirPath, pattern, ext);

                Console.WriteLine($"\n--------------------------------------------------------\n");
            }
        }
    }

}
