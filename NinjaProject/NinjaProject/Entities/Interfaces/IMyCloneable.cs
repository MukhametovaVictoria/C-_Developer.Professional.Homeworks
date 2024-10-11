
namespace Entities.Interfaces
{
    /// <summary>
    /// Техника теневого клонирования
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMyCloneable<T>
    {
        T MyClone();
    }
}
