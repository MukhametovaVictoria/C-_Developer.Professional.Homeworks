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
    public class Result
    {
        public string MethodName { get; set; }
        public string TimeString { get; set; }
        public long Ms { get; set; }
        public long Sum { get; set; }
    }

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

        public static void Start(int count)
        {
            var list = Enumerable.Range(1, count).ToList();
            var methods = new Func<List<int>, long>[] { ForSequenceSum, ForeachSequenceSum, SimpleLINQSum, ParallelListForEachSum, ParallelForEachSum, ParallelLINQSum };
            var results = new List<Result>();
            var maxNameLength = 0;
            var maxTimeLength = 0;
            var maxSumLength = 0;
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

                if (method.Method.Name.Length > maxNameLength)
                    maxNameLength = method.Method.Name.Length;
                if (result.MethodName.Length > maxTimeLength)
                    maxTimeLength = result.MethodName.Length;
                if (maxSumLength < result.Sum.ToString().Length)
                    maxSumLength = result.Sum.ToString().Length;

                results.Add(result);

            }
            var methodCaption = "Метод";
            var timeCaption = "Время выполнения в мс.";
            var sumCaption = "Сумма";
            if (maxSumLength < sumCaption.Length)
                maxSumLength = sumCaption.Length;
            Console.WriteLine($"\nРезультаты: Количество элементов массива = {count}.");
            var captions = $"|{methodCaption}{new string(' ', maxNameLength - methodCaption.Length)}|{timeCaption}|{sumCaption}{new string(' ', maxSumLength - sumCaption.Length)}|";
            Console.WriteLine($"{new string('-', captions.Length)}");
            Console.WriteLine($"{captions}");
            Console.WriteLine($"{new string('-', captions.Length)}");
            if (maxTimeLength < timeCaption.Length) maxTimeLength = timeCaption.Length;
            foreach (var result in results)
            {
                Console.WriteLine($"|{result.MethodName}{new string(' ', maxNameLength- result.MethodName.Length)}|{result.TimeString}{new string(' ', maxTimeLength - result.TimeString.Length)}|{result.Sum}{new string(' ', maxSumLength - result.Sum.ToString().Length)}|");
                Console.WriteLine($"{new string('-', captions.Length)}");
            }
        }

        private static long ForSequenceSum(List<int> list)
        {
            long result = 0;
            for (int i = 0; i < list.Count; ++i)
            {
                result += list[i];
            }
            return result;
        }

        private static long ForeachSequenceSum(List<int> list)
        {
            long result = 0;
            foreach (var x in list)
            {
                result += x;
            }
            return result;
        }

        private static long SimpleLINQSum(List<int> list)
        {
            long result = list.Select(x => (long)x).Sum();
            return  result;
        }

        private static long ParallelListForEachSum(List<int> list)
        {
            long result = 0;
            list.ForEach(x => result += x);
            return result;
        }

        private static long ParallelLINQSum(List<int> list)
        {
            var rangePartitioner = Partitioner.Create(0, list.Count);
            return rangePartitioner.AsParallel().Sum(range =>
            {
                long localSum = 0;
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    localSum += list[i];
                }
                return localSum;
            });
        }

        private static long ParallelForEachSum(List<int> list)
        {
            long result = 0;
            Parallel.ForEach(list, x => Interlocked.Add(ref result, x));
            return result;
        }
    }
}
