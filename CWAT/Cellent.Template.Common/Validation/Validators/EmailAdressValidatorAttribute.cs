using System;
using Cellent.Template.Common.Properties;

namespace Cellent.Template.Common.Validation.Validators
{
    /// <summary>
    /// EmailAdressValidatorAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class EmailAdressValidatorAttribute : RegexValidatorAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAdressValidatorAttribute"/> class.
        /// </summary>
        public EmailAdressValidatorAttribute()
            : base("^($)|^((?>[a-zA-Z\\d!#$%&'*+\\-/=?^_`{|}~]+\\x20*|\"((?=[\\x01-\\x7f])[^\"\\\\]|\\\\[\\x01-\\x7f])*\"\\x20*)*(?<angle><))?((?!\\.)(?>\\.?[a-zA-Z\\d!#$%&'*+\\-/=?^_`{|}~]+)+|\"((?=[\\x01-\\x7f])[^\"\\\\]|\\\\[\\x01-\\x7f])*\")@(((?!-)[a-zA-Z\\d\\-]+(?<!-)\\.)+[a-zA-Z]{2,}|\\[(((?(?<!\\[)\\.)(25[0-5]|2[0-4]\\d|[01]?\\d?\\d)){4}|[a-zA-Z\\d\\-]*[a-zA-Z\\d]:((?=[\\x01-\\x7f])[^\\\\\\[\\]]|\\\\[\\x01-\\x7f])+)\\])(?(angle)>)$")
        {
            Message = Resources.NoValidEmail;
        }
    }
}
