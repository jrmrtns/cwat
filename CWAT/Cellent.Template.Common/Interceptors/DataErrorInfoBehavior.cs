using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Cellent.Template.Common.Interfaces.Core;
using Cellent.Template.Common.Validation;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Cellent.Template.Common.Interceptors
{
    /// <summary>
    /// Aspect zur Validierung über zusätzliches Interface
    /// </summary>
    public class DataErrorInfoBehavior : IInterceptionBehavior
    {
        private bool _isChangeNotificationActive = true;

        /// <summary>
        /// Implement this method to execute your behavior processing.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the behavior chain.</param>
        /// <returns>
        /// Return value from the target.
        /// </returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (input.MethodBase.Name == "get_IsChangeNotificationActive")
                return input.CreateMethodReturn(_isChangeNotificationActive);

            if (input.MethodBase.Name == "set_IsChangeNotificationActive")
            {
                _isChangeNotificationActive = (bool)input.Arguments[0];
                return getNext()(input, getNext); //must be handled in last Behavior
            }

            if (input.MethodBase.Name == "Error" && input.MethodBase.DeclaringType == typeof(IDataErrorInfo))
                return input.CreateMethodReturn(string.Empty);

            if (input.MethodBase.Name == "get_Item" && input.MethodBase.DeclaringType == typeof (IDataErrorInfo))
            {
                string column = input.Inputs["columnName"] as string;
                return input.CreateMethodReturn(ValidateProperty(column, input));
            }

            return getNext()(input, getNext);
        }

        /// <summary>
        /// Returns the interfaces required by the behavior for the objects it intercepts.
        /// </summary>
        /// <returns>
        /// The required interfaces.
        /// </returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return new[] { typeof(IBaseModel) };
        }

        /// <summary>
        /// Returns a flag indicating if this behavior will actually do anything when invoked.
        /// </summary>
        /// <remarks>
        /// This is used to optimize interception. If the behaviors won't actually
        /// do anything (for example, PIAB where no policies match) then the interception
        /// mechanism can be skipped completely.
        /// </remarks>
        public bool WillExecute { get { return true; } }

        /// <summary>
        /// Validiert ein Property
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private string ValidateProperty(string propertyName, IMethodInvocation input)
        {
            if (!_isChangeNotificationActive)
                return string.Empty;

            IBaseModel model = (IBaseModel)input.Target;

            ValidationSummary validationSummary = Validator.Validate(model);

            string result = null;

            IList<ValidationEntry> validationEntries = validationSummary.Where(d => d.FieldName == propertyName).ToList();
            if (validationEntries.Any())
            {
                validationEntries.ForEach(d =>
                {
                    if (result != null)
                        result += ", ";

                    result += d.Message;
                });
            }

            model.GetType().GetProperty("IsValid").SetValue(input.Target, validationSummary.IsValid);

            return result;
        }
    }
}
