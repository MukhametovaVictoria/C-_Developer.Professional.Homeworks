using Factories.Interfaces;

namespace Factories
{
    public class MainNinjaFactory
    {
        public INinjaFactory GetNinjaFactory(int where)
        {
            switch (where)
            {
                case 1: 
                    return new KonohaNinjaFactory();
                case 2:
                    return new KiriNinjaFactory();
                case 3:
                    return new SunaNinjaFactory();
                default:
                    return new KonohaNinjaFactory();
            }
        }
    }
}
