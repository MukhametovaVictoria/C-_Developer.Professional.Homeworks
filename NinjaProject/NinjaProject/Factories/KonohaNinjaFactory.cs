using Factories.Interfaces;
using Entities;
using Entities.AbstractClasses;

namespace Factories
{
    /// <summary>
    /// Фабрика, создающего шиноби листа
    /// </summary>
    public class KonohaNinjaFactory: INinjaFactory
    {
        public BaseNinjaPrototype CreateNinja(int who)
        {
            switch (who)
            {
                case 1:
                    return new NarutoUzumaki();
                case 2:
                    return new ItachiUchiha();
                case 3:
                    return new KakashiHatake();
                case 4:
                    return new SakuraHaruno();
                default:
                    return new NarutoUzumaki();
            }
        }
    }
}
