using System;
using System.Linq;

namespace Cellent.Template.Common.Validation.Validators
{
    /// <summary>
    /// Basis für Validatoren
    /// </summary>
    public abstract class AbstractValidator : Attribute, IValidator
    {
        private string[] _groupnames = new[]{"Default"};

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        /// <value>
        /// The alias.
        /// </value>
        public String Alias { get; set; }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="field">The field.</param>
        /// <param name="summary">The summary.</param>
        /// <returns></returns>
      public abstract bool IsValid(object value, string field, ref ValidationSummary summary);

      /// <summary>
      /// Determines whether [is in group] [the specified groupname].
      /// </summary>
      /// <param name="groupname">The groupname.</param>
      /// <returns></returns>
        public bool IsInGroup(string groupname)
        {
            return _groupnames.Contains(groupname);
        }

        /// <summary>
        /// Gets or sets the groupnames.
        /// </summary>
        /// <value>
        /// The groupnames.
        /// </value>
        public string[] Groupnames
        {
            set
            {
                string[] result = new string[_groupnames.Length + value.Length];

                _groupnames.CopyTo(result, 0);
                value.CopyTo(result, _groupnames.Length);

                _groupnames = result;
            }
            get { return _groupnames; }
        }
    }
}
