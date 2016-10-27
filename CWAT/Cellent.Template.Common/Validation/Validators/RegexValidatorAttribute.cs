using System;
using System.Text.RegularExpressions;
using Cellent.Template.Common.Properties;

namespace Cellent.Template.Common.Validation.Validators
{
    /// <summary>
    /// RegexValidatorAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class RegexValidatorAttribute : ValidatorAttribute
    {
        private String _regex;
        private String _message;

        internal override String Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// Gets or sets the regex.
        /// </summary>
        /// <value>
        /// The regex.
        /// </value>
        public String Regex
        {
            get { return _regex; }
            set { _regex = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexValidatorAttribute"/> class.
        /// </summary>
        /// <param name="regex">The regex.</param>
        public RegexValidatorAttribute(String regex)
        {
            _regex = regex;
            _message = Resources.RegexValidator;
        }

        #region IValidator Member

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

            String toValidate;
            if (!(value is String))
                toValidate = value.ToString();
            else
                toValidate = (String)value;

            bool result = true;

            Regex expression = new Regex(_regex, RegexOptions.None);
            if (!expression.IsMatch(toValidate))
            {
                ValidationEntry ve = new ValidationEntry(false, String.Format(_message, new object[] { field }), field, value);
                summary.Add(ve);
                result = false;
            }

            return result;
        }

        #endregion
    }
}
