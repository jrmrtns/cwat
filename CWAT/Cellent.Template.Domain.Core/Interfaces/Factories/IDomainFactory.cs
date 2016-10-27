namespace Cellent.Template.Domain.Core.Interfaces.Factories
{
    /// <summary>
    /// Factory für Domain-Entities
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDomainFactory<T>
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns>new Instance of T</returns>
        T Create();
    }
}
