using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SerializationProject
{
    class Program
    {
        static void Main(string[] args)
        {

            var someClass = new SomeClass() { Id = Guid.NewGuid(), AuthorId = Guid.NewGuid(), Content = "", CreatedAt = DateTime.Now, Likes = 5, Title = "Title1", ShortDescription = "ghjkd", UpdatedAt = DateTime.UtcNow };
            var f = new F();

            Console.WriteLine($"Сериализация csv:");
            var csv = CsvSerialize(f);
            Console.WriteLine();
            Console.WriteLine($"Сериализация json:");
            var json = JsonSerialize(f);
            Console.WriteLine();
            Console.WriteLine($"Десериализация csv:");
            var resultCsv = CsvDeserialize<F>(csv, f.GetType());
            if(resultCsv != null)
                foreach(var obj in resultCsv)
                {
                    obj.ShowItems();
                }
            Console.WriteLine();
            Console.WriteLine($"Десериализация json:");
            var resultJson = JsonDeserialize<F>(json);
            if (resultJson != null)
                resultJson.ShowItems();

            Console.ReadKey();
        }

        public static string JsonSerialize(ISerializable obj)
        {
            var timer = new Timer();
            var result = "";
            timer.Start();
            for (int i = 0; i < 100000; i++)
            {
                result = JsonConvert.SerializeObject(obj);
            }
            var resultTime = timer.Stop();
            timer.Start();
            Console.WriteLine(result);
            Console.WriteLine($"Время выполнения {resultTime}");
            resultTime = timer.Stop();

            Console.WriteLine($"Время вывода текста {resultTime}");

            return result;
        }

        public static string CsvSerialize(ISerializable obj)
        {
            var serializer = new CsvSerializer(obj.GetType());
            var timer = new Timer();
            var result = "";
            timer.Start();
            for (int i = 0; i < 100000; i++)
            {
                result = serializer.Serialize(obj, ",");
            }
            var resultTime = timer.Stop();

            timer.Start();
            Console.WriteLine(result);
            Console.WriteLine($"Время выполнения {resultTime}");
            resultTime = timer.Stop();

            Console.WriteLine($"Время вывода текста {resultTime}");

            return result;
        }

        public static List<T> CsvDeserialize<T>(string dataStr, Type type) where T : class, new()
        {
            var serializer = new CsvSerializer(type);
            var result = new List<T>();
            var timer = new Timer();
            timer.Start();
            for (int i = 0; i < 100000; i++)
            {
                result = serializer.DeserializeFromString<T>(dataStr, ",");
            }
            var resultTime = timer.Stop();
            Console.WriteLine($"Время выполнения {resultTime}");

            return result;
        }

        public static T JsonDeserialize<T>(string dataStr) where T : class, new()
        {
            T result = null;
            var timer = new Timer();
            timer.Start();
            for (int i = 0; i < 100000; i++)
            {
                result = JsonConvert.DeserializeObject<T>(dataStr);
            }
            var resultTime = timer.Stop();
            Console.WriteLine($"Время выполнения {resultTime}");

            return result;
        }
    }
}
