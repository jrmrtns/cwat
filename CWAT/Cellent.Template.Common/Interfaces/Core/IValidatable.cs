using Cellent.Template.Common.Validation;

namespace Cellent.Template.Common.Interfaces.Core
{
    /// <summary>
    /// Zusätzliches Interface für Interception
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns></returns>
        ValidationSummary Validate();
    }
}
