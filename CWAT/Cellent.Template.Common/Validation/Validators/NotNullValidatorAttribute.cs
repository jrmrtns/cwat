using System;
using Cellent.Template.Common.Properties;

namespace Cellent.Template.Common.Validation.Validators
{
    /// <summary>
    /// NotNullValidatorAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class NotNullValidatorAttribute : ValidatorAttribute
    {
        private String _message;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullValidatorAttribute"/> class.
        /// </summary>
        public NotNullValidatorAttribute()
        {
            _message = Resources.FieldCannotBeNotNull;
        }

        internal override String Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="field">The field.</param>
        /// <param name="summary">The summary.</param>
        /// <returns></returns>
        public override bool IsValid(object value, string field, ref ValidationSummary summary)
        {
            if (value == null)
            {
                ValidationEntry ve = new ValidationEntry(false, String.Format(_message, new object[] { Alias ?? field }), field, null);
                summary.Add(ve);
                return false;
            }

            return true;
        }
    }
}
