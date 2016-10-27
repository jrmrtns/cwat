using System;
using Cellent.Template.Common.Properties;

namespace Cellent.Template.Common.Validation.Validators
{
    /// <summary>
    /// StringLengthValidatorAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class StringLengthValidatorAttribute : AbstractValidator
    {
        private int _minLen;
        private int _maxLen;

        /// <summary>
        /// Gets or sets the maximum length.
        /// </summary>
        /// <value>
        /// The maximum length.
        /// </value>
        public int MaxLen
        {
            get { return _maxLen; }
            set { _maxLen = value; }
        }

        /// <summary>
        /// Gets or sets the minimum length.
        /// </summary>
        /// <value>
        /// The minimum length.
        /// </value>
        public int MinLen
        {
            get { return _minLen; }
            set { _minLen = value; }
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
                return false;

            if (!(value is String))
                return false;

            String s = (String)value;
            bool result = true;

            if (s.Length < _minLen)
            {
                ValidationEntry ve = new ValidationEntry(false, String.Format(Resources.MinLengthFallenBelow, new object[] { _minLen, field }), field, value);
                summary.Add(ve);
                result = false;
            }

            if (s.Length > _maxLen)
            {
                ValidationEntry ve = new ValidationEntry(false, String.Format(Resources.MaxLengthExceeded, new object[] { _maxLen, field }), field, value);
                summary.Add(ve);
                result = false;
            }

            return result;
        }
    }
}
