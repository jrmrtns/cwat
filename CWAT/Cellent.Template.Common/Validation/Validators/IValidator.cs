using System;

namespace Cellent.Template.Common.Validation.Validators
{
    /// <summary>
    /// Basisinterface für Validatoren
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="field">The field.</param>
        /// <param name="summary">The summary.</param>
        /// <returns></returns>
        bool IsValid(object value, string field, ref ValidationSummary summary);

        /// <summary>
        /// Determines whether [is in group] [the specified groupname].
        /// </summary>
        /// <param name="groupname">The groupname.</param>
        /// <returns></returns>
        bool IsInGroup(string groupname);

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        /// <value>
        /// The alias.
        /// </value>
        String Alias { get; set; }
    }
}
