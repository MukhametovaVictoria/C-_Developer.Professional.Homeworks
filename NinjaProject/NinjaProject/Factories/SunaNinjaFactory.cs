using Factories.Interfaces;
using Entities;
using Entities.AbstractClasses;
using Enums;

namespace Factories
{
    /// <summary>
    /// Фабрика, создающая шиноби песка
    /// </summary
    public class SunaNinjaFactory : INinjaFactory
    {
        public BaseNinjaPrototype CreateNinja(int who)
        {
            switch (who)
            {
                case 1:
                    return new GaaraSobakuno();
                case 2:
                    return new TemariSobakuno();
                case 3:
                    return new ChiyoBaasama();
                default:
                    return new GaaraSobakuno();
            }
        }
    }
}
