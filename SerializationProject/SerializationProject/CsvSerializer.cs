using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SerializationProject
{
    public class CsvIgnoreAttribute : Attribute { }

    public class CsvSerializer
    {
        private List<PropertyInfo> _properties;
        private List<FieldInfo> _fields;

        public CsvSerializer(Type type)
        {
            _properties = type.GetProperties(BindingFlags.Instance |
                   BindingFlags.NonPublic |
                   BindingFlags.Public).Where(a => a.GetCustomAttribute<CsvIgnoreAttribute>() == null).ToList();
            _fields = type.GetFields(BindingFlags.Instance |
                   BindingFlags.NonPublic |
                   BindingFlags.Public).Where(a => a.GetCustomAttribute<CsvIgnoreAttribute>() == null).ToList();
        }

        public string Serialize(ISerializable[] entities, string separator)
        {
            if (entities == null || entities.Length == 0)
                return "";

            var dataKeys = "";
            var dataValues = new List<string>();
            foreach (var entity in entities)
            {
                var values = GetKeyValuePairInCsvFormat(entity, separator);

                if (String.IsNullOrEmpty(dataKeys))
                    dataKeys = values.Item1;

                dataValues.Add(values.Item2);
            }

            return dataKeys + "\n" + String.Join("\n", dataValues);
        }

        public string Serialize(ISerializable entity, string separator)
        {
            if (entity == null)
                return "";

            var values = GetKeyValuePairInCsvFormat(entity, separator);

            return values.Item1 + "\n" + values.Item2;
        }

        public List<T> DeserializeFromString<T>(string dataStr, string separator) where T : class, new()
        {
            if (String.IsNullOrEmpty(dataStr) || (_properties.Count == 0 && _fields.Count == 0))
                return null;

            var rows = dataStr.Split('\n').Select(x => x.Trim()).ToArray();
            if (rows.Length == 0)
                return null;

            var sep = new string[] { separator };
            var columns = rows[0].Split(sep, StringSplitOptions.RemoveEmptyEntries);
            if (columns.Length == 0)
                return null;

            var data = new List<T>();
            for (var i = 1; i < rows.Length; i++)
            {
                var values = rows[i].Split(sep, StringSplitOptions.None);
                var obj = new T();
                for (int j = 0; j < columns.Length; j++)
                {
                    var value = values[j];
                    var column = columns[j];

                    var p = _properties.FirstOrDefault(a => a.Name == column);
                    if (p != null)
                    {
                        var converter = TypeDescriptor.GetConverter(p.PropertyType);
                        var convertedValue = converter.ConvertFrom(value);

                        p.SetValue(obj, convertedValue);
                    }

                    var f = _fields.FirstOrDefault(a => a.Name == column);
                    if (f != null)
                    {
                        var converter = TypeDescriptor.GetConverter(f.FieldType);
                        var convertedValue = converter.ConvertFrom(value);

                        f.SetValue(obj, convertedValue);
                    }
                }
                data.Add(obj);
            }

            return data;
        }

        private Tuple<string, string> GetKeyValuePairInCsvFormat(ISerializable entity, string separator)
        {
            if (entity == null)
                return null;

            StreamingContext streamingContext = new StreamingContext(StreamingContextStates.Persistence);
            var info = new SerializationInfo(entity.GetType(), new FormatterConverter());
            entity.GetObjectData(info, streamingContext);
            var keys = new string[info.MemberCount];
            var values = new object[info.MemberCount];
            var j = 0;
            foreach (var entry in info)
            {
                keys[j] = entry.Name;
                values[j] = entry.Value;
                j++;
            }

            var dataKeys = String.Join(separator, keys);

            var dataValues = String.Join(separator, values.Select(x => x != null ? x.ToString() : ""));

            return new Tuple<string, string>(dataKeys, dataValues);
        }
    }
}
