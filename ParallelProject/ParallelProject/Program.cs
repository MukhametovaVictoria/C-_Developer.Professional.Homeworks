using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProject
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowInfo();
            Start(100000);
            Start(1000000);
            Start(10000000);

            Console.ReadKey();
        }

        public static void Start(int count)
        {
            var list = Enumerable.Range(1, count).Select(x => (long)x).ToList();
            var methods = new Func<List<long>, long>[] { ForSequenceSum, ForeachSequenceSum, SimpleLINQSum, 
                ParallelForEachSum, ParallelLINQSum, ParallelListForEachSumWithChunkify };
            var results = new List<Result>();
            var length = new Length();
            foreach (var method in methods)
            {
                var result = new Result()
                {
                    MethodName = method.Method.Name
                };

                Stopwatch stopwatch = Stopwatch.StartNew();
                result.Sum = method(list);
                stopwatch.Stop();

                result.TimeString = stopwatch.ElapsedMilliseconds.ToString();
                result.Ms = stopwatch.ElapsedMilliseconds;

                SetLength(result.MethodName, result.TimeString, result.Sum.ToString(), length);

                results.Add(result);
            }

            Additional(length, results, list);
            ShowResults(length, results, count);
        }

        //Вынесено в отдельный метод для проверки скорости счета без учета разделения списка на части
        private static void Additional(Length length, List<Result> results, List<long> list)
        {
            var res = new Result()
            {
                MethodName = "ParallelListForEachSum"
            };
            var chunks = list.Chunkify(1000).ToList();
            Stopwatch sw = Stopwatch.StartNew();
            res.Sum = ParallelListForEachSum(chunks);
            sw.Stop();
            res.TimeString = sw.ElapsedMilliseconds.ToString();
            res.Ms = sw.ElapsedMilliseconds;
            SetLength(res.MethodName, res.TimeString, res.Sum.ToString(), length);
            results.Add(res);
        }

        private static long ForSequenceSum(List<long> list)
        {
            long result = 0;
            for (int i = 0; i < list.Count; ++i)
            {
                result += list[i];
            }
            return result;
        }

        private static long ForeachSequenceSum(List<long> list)
        {
            long result = 0;
            foreach (var x in list)
            {
                result += x;
            }
            return result;
        }

        private static long SimpleLINQSum(List<long> list)
        {
            long result = list.Sum();
            return  result;
        }

        private static long ParallelListForEachSum(List<IEnumerable<long>> chunks)
        {
            var tasks = new Task[chunks.Count()];
            var results = new long[chunks.Count()];
            for (var i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Factory.StartNew((Object obj) =>
                {
                    var num = (int)obj;
                    results[num] = chunks[num].Sum();
                }, i);
            }
            Task.WaitAll(tasks);
            return results.Sum();
        }

        private static long ParallelListForEachSumWithChunkify(List<long> list)
        {
            var chunks = list.Chunkify(1000).ToList();
            return ParallelListForEachSum(chunks);
        }

        private static long ParallelLINQSum(List<long> list)
        {
            return list.AsParallel().WithDegreeOfParallelism(5).Sum();
        }

        private static long ParallelForEachSum(List<long> list)
        {
            long result = 0;
            Parallel.ForEach(list, x => Interlocked.Add(ref result, x));
            return result;
        }

        private static void SetLength(string methodName, string time, string sum, Length length)
        {
            if (methodName.Length > length.NameLength)
                length.NameLength = methodName.Length;
            if (time.Length > length.TimeLength)
                length.TimeLength = time.Length;
            if (length.SumLength < sum.Length)
                length.SumLength = sum.Length;
        }

        private static void ShowResults(Length length, List<Result> results, int count)
        {
            var methodCaption = "Метод";
            var timeCaption = "Время выполнения в мс.";
            var sumCaption = "Сумма";
            if (length.SumLength < sumCaption.Length)
                length.SumLength = sumCaption.Length;
            Console.WriteLine($"\nРезультаты: Количество элементов массива = {count}.");
            var captions = $"|{methodCaption}{new string(' ', length.NameLength - methodCaption.Length)}|{timeCaption}|{sumCaption}{new string(' ', length.SumLength - sumCaption.Length)}|";
            Console.WriteLine($"{new string('-', captions.Length)}");
            Console.WriteLine($"{captions}");
            Console.WriteLine($"{new string('-', captions.Length)}");
            if (length.TimeLength < timeCaption.Length) length.TimeLength = timeCaption.Length;
            foreach (var result in results)
            {
                Console.WriteLine($"|{result.MethodName}{new string(' ', length.NameLength - result.MethodName.Length)}|{result.TimeString}{new string(' ', length.TimeLength - result.TimeString.Length)}|{result.Sum}{new string(' ', length.SumLength - result.Sum.ToString().Length)}|");
                Console.WriteLine($"{new string('-', captions.Length)}");
            }
        }

        public static void ShowInfo()
        {
            ManagementObjectSearcher searcher5 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");

            foreach (ManagementObject queryObj in searcher5.Get())
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Win32_OperatingSystem instance");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("BuildNumber: {0}", queryObj["BuildNumber"]);
                Console.WriteLine("Caption: {0}", queryObj["Caption"]);
                Console.WriteLine("FreePhysicalMemory: {0}", queryObj["FreePhysicalMemory"]);
                Console.WriteLine("FreeVirtualMemory: {0}", queryObj["FreeVirtualMemory"]);
                Console.WriteLine("Name: {0}", queryObj["Name"]);
                Console.WriteLine("OSType: {0}", queryObj["OSType"]);
                Console.WriteLine("RegisteredUser: {0}", queryObj["RegisteredUser"]);
                Console.WriteLine("SerialNumber: {0}", queryObj["SerialNumber"]);
                Console.WriteLine("ServicePackMajorVersion: {0}", queryObj["ServicePackMajorVersion"]);
                Console.WriteLine("ServicePackMinorVersion: {0}", queryObj["ServicePackMinorVersion"]);
                Console.WriteLine("Status: {0}", queryObj["Status"]);
                Console.WriteLine("SystemDevice: {0}", queryObj["SystemDevice"]);
                Console.WriteLine("SystemDirectory: {0}", queryObj["SystemDirectory"]);
                Console.WriteLine("SystemDrive: {0}", queryObj["SystemDrive"]);
                Console.WriteLine("Version: {0}", queryObj["Version"]);
                Console.WriteLine("WindowsDirectory: {0}", queryObj["WindowsDirectory"]);
            }
            Console.WriteLine();

            ManagementObjectSearcher searcher8 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

            foreach (ManagementObject queryObj in searcher8.Get())
            {
                Console.WriteLine("------------- Win32_Processor instance ---------------");
                Console.WriteLine("Name: {0}", queryObj["Name"]);
                Console.WriteLine("NumberOfCores: {0}", queryObj["NumberOfCores"]);
                Console.WriteLine("ProcessorId: {0}", queryObj["ProcessorId"]);
            }

            Console.WriteLine();

            ManagementObjectSearcher searcher12 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");

            Console.WriteLine("------------- Win32_PhysicalMemory instance --------");
            foreach (ManagementObject queryObj in searcher12.Get())
            {
                Console.WriteLine("BankLabel: {0} ; Capacity: {1} Gb; Speed: {2} ", queryObj["BankLabel"],
                                  Math.Round(System.Convert.ToDouble(queryObj["Capacity"]) / 1024 / 1024 / 1024, 2),
                                   queryObj["Speed"]);
            }

            Console.WriteLine();
        }
    }

    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> Chunkify<T>(this IEnumerable<T> source, int size)
        {
            int count = 0;
            using (var iter = source.GetEnumerator())
            {
                while (iter.MoveNext())
                {
                    var chunk = new T[size];
                    count = 1;
                    chunk[0] = iter.Current;
                    for (int i = 1; i < size && iter.MoveNext(); i++)
                    {
                        chunk[i] = iter.Current;
                        count++;
                    }
                    if (count < size)
                    {
                        Array.Resize(ref chunk, count);
                    }
                    yield return chunk;
                }
            }
        }
    }
    public class Result
    {
        public string MethodName { get; set; }
        public string TimeString { get; set; }
        public long Ms { get; set; }
        public long Sum { get; set; }
    }

    public class Length
    {
        public int TimeLength { get; set; }
        public int NameLength { get; set; }
        public int SumLength { get; set; }
    }
}
