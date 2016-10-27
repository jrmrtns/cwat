namespace Cellent.Template.Domain.Core.Interfaces
{
    /// <summary>
    /// Factory to create DB Context
    /// </summary>
    public interface IContextFactory
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        BaseContext Create();
    }
}