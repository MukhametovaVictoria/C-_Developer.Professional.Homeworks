using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDProject.GameFolder
{
    /// <summary>
    /// Базовый класс настроек
    /// Реализация принципа D - Dependency inversion И O - Open/Closed
    /// Можно наследоваться от этого класса и реализовывать новые кастомные классы настроек, а затем передавать их в игру
    /// Данный класс не нужно менять, но его можно расширять
    /// </summary>
    abstract class BaseSettings
    {
        private int[] _range = Constants.Range;
        private int _attempts = Constants.Attempts;
        private string _currentClassName;

        public int[] Range { get => _range; set => _range = value; }
        public int Attempts { get => _attempts; set => _attempts = value; }
        public string CurrentClassName { get => _currentClassName; }

        public BaseSettings() 
        {
            _currentClassName = this.GetType().ToString();
        }

        public BaseSettings(BaseSettings settings)
        {
            if(settings != null)
            {
                if (settings.Range != null && settings.Range.Length == 2)
                    Range = settings.Range;

                if (settings.Attempts > 0)
                    Attempts = settings.Attempts;

                if (!String.IsNullOrEmpty(settings.CurrentClassName))
                    _currentClassName = settings.CurrentClassName;
            }
        }

        public abstract BaseSettings UpdateSettings();

        public BaseSettings ReturnBaseSettings()
        {
            Range = Constants.Range;
            Attempts = Constants.Attempts;

            return this;
        }
    }
}
