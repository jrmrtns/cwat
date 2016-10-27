using System;
using Cellent.Template.Common.Validation.Validators;

namespace Cellent.Template.Common.Validation
{
    /// <summary>
    /// ValidatorAttribute
    /// </summary>
    public abstract class ValidatorAttribute : AbstractValidator
    {
        internal abstract String Message
        {
            get;
            set;
        }
    }
}
