using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDProject.GameFolder
{
    class LoggerService : ITextWriter
    {
        private readonly string _filePath;
        public string FilePath { get => _filePath; }

        public LoggerService(string filePath)
        {
            _filePath = filePath;
        }

        public void WriteToFile(string text)
        {
            StreamWriter log;

            if (!File.Exists(FilePath))
            {
                log = new StreamWriter(FilePath);
            }
            else
            {
                log = File.AppendText(FilePath);
            }

            log.WriteLine(DateTime.Now);
            log.WriteLine(text);
            log.WriteLine();

            log.Close();
        }
    }
}
