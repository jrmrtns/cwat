using Cellent.Template.Common.Properties;
using System;

namespace Cellent.Template.Common.Validation.Validators
{
    /// <summary>
    /// class DateTimeStringScheduleValidator
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DateTimeStringScheduleValidator: AbstractValidator
    {
        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="field">The field.</param>
        /// <param name="summary">The summary.</param>
        public override bool IsValid(object value, string field, ref ValidationSummary summary)
        {
            string dayText = value as string;
            DateTime day = DateTime.Now;
            
            if(DateTime.TryParse(dayText, out day))
            {
                int result = DateTime.Compare(day, DateTime.Now);
                if (result < 0)
                {
                    ValidationEntry ve = new ValidationEntry(false, String.Format(Resources.NoValidDate, new[] { field }), field, value);
                    summary.Add(ve);

                    return false;
                }
                return true;
            }

            return false;
        }
    }
}
