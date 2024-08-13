using System;
using System.Collections.Generic;
using System.IO;

namespace DelegatesProj
{
    class FileSearcher
    {
        private List<FileInfo> _files;
        private string _extension;

        public event EventHandler<FileArgs> FileFound;

        /// <summary>
        /// Метод запуска процесса поиска файлов в указанном каталоге
        /// </summary>
        /// <param name="dirPath">Путь до каталога</param>
        /// <param name="pattern">Паттерн поиска файлов</param>
        /// <param name="ext">Расширение, на котором прервать процесс поиска</param>
        public void RunProcess(string dirPath, string pattern = "*", string ext = "")
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            if (!dirInfo.Exists)
            {
                Console.WriteLine("Каталога не существует.");
                return;
            }

            _files = new List<FileInfo>();
            _extension = ext;

            Search(dirInfo, pattern);
            
            if(_files.Count > 0)
            {
                var max = _files.GetMax(ConverterToFloat.ConvertFileInfoToFloat);
                Console.WriteLine($"\nРезультат выбора максимального: ");
                Console.WriteLine($"Имя файла: \"{max?.Name}\", директория: {max?.Directory}, размер: {max?.Length} байт.");
            }
            Console.WriteLine($"\nВсего найдено файлов: {_files.Count}.");
        }

        /// <summary>
        /// Поиск файлов
        /// </summary>
        /// <param name="directory">Директория поиска</param>
        /// <param name="searchPattern">Паттерн поиска файлов</param>
        /// <returns>Продолжать поиск Да/Нет</returns>
        private bool Search(DirectoryInfo directory, string searchPattern)
        {
            // Получаем все файлы в текущем каталоге
            try
            {
                var files = directory.GetFiles(searchPattern);

                foreach (FileInfo fi in files)
                {
                    var fileArgs = InvokeEvent(fi);
                    if (fileArgs.IsFinal)
                        return false;
                }

                //получаем все подкаталоги
                var subDirs = directory.GetDirectories();

                //проходим по каждому подкаталогу
                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    //РЕКУРСИЯ с прерыванием цикла в случае false
                    if (!Search(dirInfo, searchPattern))
                        return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return true;
        }

        /// <summary>
        /// Вызов события 
        /// </summary>
        /// <param name="file">Файл</param>
        /// <returns>Аргументы события</returns>
        private FileArgs InvokeEvent(FileInfo file)
        {
            var args = new FileArgs(file) { IsFinal = false};
            FileFound?.Invoke(this, args);

            _files.Add(file);
            //Возможность отмены дальнейшего поиска из обработчика
            if (!String.IsNullOrEmpty(_extension) && args.FileName.Contains(_extension))
            {
                args.IsFinal = true;
            }

            return args;
        }
    }
}
