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

            FileFound += OnFileFound;

            Search(dirInfo, pattern);

            FileFound -= OnFileFound;
            
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
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;
            // Получаем все файлы в текущем каталоге
            try
            {
                files = directory.GetFiles(searchPattern);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (FileInfo fi in files)
                {
                    var fileArgs = InvokeEvent(fi);
                    Console.WriteLine($"Найден файл {fileArgs.FileName}.");

                    //Возможность отмены дальнейшего поиска из обработчика
                    if (!String.IsNullOrEmpty(_extension) && fileArgs.FileName.Contains(_extension))
                    { 
                        return false; 
                    }
                }

                //получаем все подкаталоги
                subDirs = directory.GetDirectories();
                if(subDirs != null)
                {
                    //проходим по каждому подкаталогу
                    foreach (DirectoryInfo dirInfo in subDirs)
                    {
                        //РЕКУРСИЯ с прерыванием цикла в случае false
                        if (!Search(dirInfo, searchPattern))
                            return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Подписка на событие нахождения файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void OnFileFound(object sender, EventArgs eventArgs)
        {
            var fileArgs = (FileArgs)eventArgs;
            _files.Add(fileArgs.FileInfo);
        }

        /// <summary>
        /// Вызов события 
        /// </summary>
        /// <param name="file">Файл</param>
        /// <returns>Аргументы события</returns>
        private FileArgs InvokeEvent(FileInfo file)
        {
            var args = new FileArgs(file);
            FileFound?.Invoke(this, args);
            return args;
        }
    }
}
