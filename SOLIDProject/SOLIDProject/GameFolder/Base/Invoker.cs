using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDProject.GameFolder
{
    /// <summary>
    /// Класс вызова методов по определенному пути
    /// Реализация принципа S -Single responsibility
    /// Класс занимается только вызовом методов
    /// </summary>
    class Invoker
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public Dictionary<string, object> ConstructorPrameters { get; set; }
        public Dictionary<string, object> MethodParameters { get; set; }

        public object InvokeMethod()
        {
            var classType = Type.GetType(ClassName, false, false);

            if (classType == null)
            {
                throw new Exception("Класс по пути '" + ClassName + "' не найден");
            }

            MethodInfo[] methodArr = classType.GetMethods();

            var method = methodArr.Where(m => m.Name == MethodName).ToArray().FirstOrDefault();

            if (method == null)
            {
                throw new Exception("Метод '" + MethodName + "' не найден в классе '" + classType.Name + "'");
            }

            var constructors = classType.GetConstructors();
            if(constructors == null || constructors.Length == 0)
                throw new Exception("Не найден конструктор в классе '" + classType.Name + "'");

            object handler = null;

            //Проходим по конструкторам, пытаемся подобрать подходящий в зависимости от параметров
            foreach (var constr in constructors)
            {
                try
                {
                    //Считываем сколько принимает параметров конструктор
                    var parameters = constr.GetParameters();
                    
                    //Были переданы параметры
                    if (ConstructorPrameters != null && ConstructorPrameters.Count > 0)
                    {
                        if (parameters == null || parameters.Length == 0)
                            continue;
                        handler = constr.Invoke(ConstructorPrameters.Values.ToArray());
                    }
                    else //Нет параметров
                    {
                        if (parameters != null && parameters.Length > 0)
                            continue;
                        handler = constr.Invoke(new object[] { });
                    }
                }
                catch(Exception ex)
                {
                    continue;
                }
            }

            if(handler == null)
                throw new Exception("Не найден подходящий конструктор в классе '" + classType.Name + "'");

            if (MethodParameters != null && MethodParameters.Count > 0)
                return method.Invoke(handler, MethodParameters.Values.ToArray());

            return method.Invoke(handler, new object[] { });
        }
    }
}
