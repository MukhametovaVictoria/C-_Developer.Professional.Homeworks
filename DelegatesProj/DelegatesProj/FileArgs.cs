using System;
using System.IO;

namespace DelegatesProj
{
    /// <summary>
    /// Аргументы события
    /// </summary>
    class FileArgs : EventArgs
    {
        /// <summary>
        /// Название файла
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Информация о файле
        /// </summary>
        public FileInfo FileInfo { get; }

        public bool IsFinal { get; set; }

        public FileArgs(FileInfo fileInfo)
        {
            FileName = fileInfo.FullName;
            FileInfo = fileInfo;
        }
    }
}
