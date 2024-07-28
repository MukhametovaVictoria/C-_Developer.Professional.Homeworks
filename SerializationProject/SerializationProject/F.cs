using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SerializationProject
{
    [Serializable]
    class F : ISerializable
    {
        int i1, i2, i3, i4, i5;

        public F()
        { }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private F(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            i1 = info.GetInt32("i1");
            i2 = info.GetInt32("i2");
            i3 = info.GetInt32("i3");
            i4 = info.GetInt32("i4");
            i5 = info.GetInt32("i5");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            var f = Get();
            info.AddValue("i1", f.i1);
            info.AddValue("i2", f.i2);
            info.AddValue("i3", f.i3);
            info.AddValue("i4", f.i4);
            info.AddValue("i5", f.i5);
        }

        F Get() => new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };

        public void ShowItems()
        {
            Console.WriteLine($"i1={i1}, i2={i2}, i3={i3}, i4={i4}, i5={i5}");
        }
    }
}
