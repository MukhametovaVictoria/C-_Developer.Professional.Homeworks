using Entities.AbstractClasses;

namespace Factories.Interfaces
{
    /// <summary>
    /// Интерфейс фабрики, создающей персонажа шиноби
    /// </summary
    public interface INinjaFactory
    {
        BaseNinjaPrototype CreateNinja(int who);
    }
}
