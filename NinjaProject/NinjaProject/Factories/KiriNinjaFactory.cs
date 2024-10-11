using Entities;
using Entities.AbstractClasses;
using Factories.Interfaces;

namespace Factories
{
    /// <summary>
    /// Фабрика, создающая шиноби тумана
    /// </summary
    public class KiriNinjaFactory: INinjaFactory
    {
        public BaseNinjaPrototype CreateNinja(int who)
        {
            switch (who)
            {
                case 1:
                    return new SuigetsuHōzuki();
                case 2:
                    return new KisameHoshigaki();
                default:
                    return new SuigetsuHōzuki();
            }
        }
    }
}
