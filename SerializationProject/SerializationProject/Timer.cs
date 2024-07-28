using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationProject
{
    public class Timer
    {
        private System.Diagnostics.Stopwatch stopwatch;

        public void Start()
        {
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
        }

        public string Stop()
        {
            stopwatch.Stop();
            var resultTime = stopwatch.Elapsed;

            // elapsedTime - строка, которая будет содержать значение затраченного времени
            return String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                resultTime.Hours,
                resultTime.Minutes,
                resultTime.Seconds,
                resultTime.Milliseconds);
        }
    }
}
