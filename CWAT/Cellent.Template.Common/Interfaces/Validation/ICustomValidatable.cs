namespace Cellent.Template.Common.Interfaces.Validation
{
    /// <summary>
    /// Interface für benutzerdefinierte Validierung
    /// </summary>
    public interface ICustomValidatable
    {
        /// <summary>
        /// Executes the custom validation.
        /// </summary>
        /// <param name="propertyName">Name of the column.</param>
        /// <returns></returns>
        string ExecuteCustomValidation(string propertyName);
    }
}
