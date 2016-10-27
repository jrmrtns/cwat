using System;
using Cellent.Template.Common.Properties;

namespace Cellent.Template.Common.Validation.Validators
{
    /// <summary>
    /// Validiert Bereiche
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class NumberValidatorAttribute : AbstractValidator
    {
        /// <summary>
        /// 
        /// </summary>
        public enum Restriction
        {
            /// <summary>
            /// The less
            /// </summary>
            Less,
            /// <summary>
            /// The less or equal
            /// </summary>
            LessOrEqual,
            /// <summary>
            /// The equal
            /// </summary>
            Equal,
            /// <summary>
            /// The greater or equal
            /// </summary>
            GreaterOrEqual,
            /// <summary>
            /// The greater
            /// </summary>
            Greater
        }
        
        private readonly Restriction _restriction;
        private readonly object _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberValidatorAttribute"/> class.
        /// </summary>
        /// <param name="restriction">The restriction.</param>
        /// <param name="value">The value.</param>
        public NumberValidatorAttribute(Restriction restriction, object value)
        {
            _value = value;
            _restriction = restriction;
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

            if (!(value is IComparable))
                return false;

            IComparable toValidate = (IComparable)value;
            
            bool isValid = true;
            string message = string.Empty;
            
            switch (_restriction)
            {
                case Restriction.Less:
                    if (toValidate.CompareTo(_value) >= 0)
                    {
                        isValid = false;
                        message = Resources.NumberValidatorLess;
                    }
                    break;
                case Restriction.LessOrEqual:
                    if (toValidate.CompareTo(_value) > 0)
                    {
                        isValid = false;
                        message = Resources.NumberValidatorLessEqual;
                    }
                    break;
                case Restriction.Equal:
                    if (toValidate.CompareTo(_value) != 0)
                    {
                        isValid = false;
                        message = Resources.NumberValidatorEqual;
                    }
                    break;
                case Restriction.Greater:
                    if (toValidate.CompareTo(_value) <= 0)
                    {
                        isValid = false;
                        message = Resources.NumberValidatorGreater;
                    }
                    break;
                case Restriction.GreaterOrEqual:
                    if (toValidate.CompareTo(_value) < 0)
                    {
                        isValid = false;
                        message = Resources.NumberValidatorGreaterEqual;
                    }
                    break;
            }

            if(isValid == false)
            {
                ValidationEntry ve = new ValidationEntry(false, String.Format(message, new[] { field, _value }), field, _value);
                summary.Add(ve);
            }
            return isValid;
        }
    }
}
