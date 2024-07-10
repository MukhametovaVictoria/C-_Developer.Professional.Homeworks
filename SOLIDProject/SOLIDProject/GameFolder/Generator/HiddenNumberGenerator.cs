using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDProject.GameFolder
{
    /// <summary>
    /// Генерация скрытых чисел
    /// Реализация принципа I - Interface segregation
    /// Наследуемся от интерфейса IGenerator и реализовываем метод GenerateNumber по своему, в зависимости от смысла класса
    /// </summary>
    class HiddenNumberGenerator : IGenerator
    {
        private readonly BaseSettings _settings;

        public HiddenNumberGenerator(BaseSettings settings)
        {
            _settings = settings;
        }
        public int GenerateNumber()
        {
            Random random = new Random();
            return random.Next(_settings.Range[0], _settings.Range[1]);
        }
    }
}
