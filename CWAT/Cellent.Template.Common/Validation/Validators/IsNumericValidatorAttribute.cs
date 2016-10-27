using System;
using Cellent.Template.Common.Properties;

namespace Cellent.Template.Common.Validation.Validators
{
    /// <summary>
    /// IsNumericValidatorAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class IsNumericValidatorAttribute : RegexValidatorAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsNumericValidatorAttribute"/> class.
        /// </summary>
        public IsNumericValidatorAttribute()
            :base("[-]?\\d*[.,]?\\d+")
        {
            Message = Resources.IsNotNummeric;
        }
    }
}
