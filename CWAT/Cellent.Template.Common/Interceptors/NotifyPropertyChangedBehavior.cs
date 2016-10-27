using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Cellent.Template.Common.Interceptors.Helper;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Cellent.Template.Common.Interceptors
{
    /// <summary>
    /// Aspect zur Validierung über zusätzliches Interface
    /// </summary>
    public class NotifyPropertyChangedBehavior : IInterceptionBehavior
    {
        #region Fields (2) 

        private bool _isChangeNotificationActive = true;

        private static readonly MethodInfo AddEventMethodInfo =
           typeof(INotifyPropertyChanged).GetEvent("PropertyChanged").GetAddMethod();

        private static readonly MethodInfo RemoveEventMethodInfo =
            typeof(INotifyPropertyChanged).GetEvent("PropertyChanged").GetRemoveMethod();

        #endregion Fields 

        #region Properties (1) 

        /// <summary>
        /// Returns a flag indicating if this behavior will actually do anything when invoked.
        /// </summary>
        /// <remarks>
        /// This is used to optimize interception. If the behaviors won't actually
        /// do anything (for example, PIAB where no policies match) then the interception
        /// mechanism can be skipped completely.
        /// </remarks>
        public bool WillExecute { get { return true; } }

        #endregion Properties 

        #region Delegates and Events (1) 

        #region Events (1) 

        /// <summary>
        /// Wird gefeuert, wenn sich der Wert der Eigenschft ändert
        /// </summary>
        private event PropertyChangedEventHandler PropertyChanged;

        #endregion Events 

        #endregion Delegates and Events 

        #region Methods (7) 

        #region Public Methods (2) 

        /// <summary>
        /// Returns the interfaces required by the behavior for the objects it intercepts.
        /// </summary>
        /// <returns>
        /// The required interfaces.
        /// </returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

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
            if (input.MethodBase == AddEventMethodInfo)
                return AddEventSubscription(input);

            if (input.MethodBase == RemoveEventMethodInfo)
                return RemoveEventSubscription(input);

            if (input.MethodBase.Name == "get_IsChangeNotificationActive")
                return input.CreateMethodReturn(_isChangeNotificationActive);

            if (input.MethodBase.Name == "set_IsChangeNotificationActive")
            {
                _isChangeNotificationActive = (bool) input.Arguments[0];
                return input.CreateMethodReturn(null);
            }

            if (_isChangeNotificationActive && IsSetter(input) && IsChange(input))
            {
                IMethodReturn result = getNext()(input, getNext);
                string propertyName = input.MethodBase.Name.Substring(4);

                NotifyPropertyChanged(propertyName, input);
                NotifyAdditionalProperties(input, propertyName);

                return result;
            }
            return getNext()(input, getNext);
        }

        private void NotifyAdditionalProperties(IMethodInvocation input, string propertyName)
        {
            IEnumerable<OnPropertyChangedAttribute> attributes = new List<OnPropertyChangedAttribute>();
            MetadataTypeAttribute metadataType = input.Target.GetType().GetCustomAttribute<MetadataTypeAttribute>();
            if (metadataType != null)
            {
                PropertyInfo propertyInfo = metadataType.MetadataClassType.GetProperty(propertyName);
                if (propertyInfo != null)
                    attributes = propertyInfo.GetCustomAttributes<OnPropertyChangedAttribute>(true);
            }

            attributes = attributes
                .Union(input.Target.GetType()
                    .GetProperty(propertyName)
                    .GetCustomAttributes<OnPropertyChangedAttribute>(true));

            foreach (OnPropertyChangedAttribute propertyChangeAttribute in attributes)
            {
                NotifyPropertyChanged(propertyChangeAttribute.PropertyName, input);
            }
        }

        #endregion Public Methods 

        #region Protected Methods (1) 

        /// <summary>
        /// feuert das PropertyChangedEventHandler event
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="input"></param>
        protected void NotifyPropertyChanged(String propertyName, IMethodInvocation input)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(input.Target, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion Protected Methods 

        #region Private Methods (4) 

        /// <summary>
        /// Adds the event subscription.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private IMethodReturn AddEventSubscription(IMethodInvocation input)
        {
            var subscriber = (PropertyChangedEventHandler)input.Arguments[0];
            PropertyChanged += subscriber;
            return input.CreateMethodReturn(null);
        }

        /// <summary>
        /// Determines whether the specified input is a change.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private static bool IsChange(IMethodInvocation input)
        {
            var propertyName = input.MethodBase.Name.Substring(4);

            object[] propertyIndex = input
                .Arguments.Cast<object>()
                .Where((a, index) => index < input.Arguments.Count - 1)
                .ToArray();

            object oldValue = input.Target.GetType().GetProperty(propertyName).GetValue(input.Target, propertyIndex);

            object newValue = input.Arguments[input.Arguments.Count - 1];

            if (oldValue == null && newValue == null)
                return false;

            if (oldValue != null)
                return !oldValue.Equals(newValue);

            return true;
        }

        /// <summary>
        /// Determines whether the specified input is setter.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private static bool IsSetter(IMethodInvocation input)
        {
            return input.MethodBase.IsSpecialName && input.MethodBase.Name.StartsWith("set_");
        }

        /// <summary>
        /// Removes the event subscription.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private IMethodReturn RemoveEventSubscription(IMethodInvocation input)
        {
            var subscriber = (PropertyChangedEventHandler)input.Arguments[0];
            PropertyChanged -= subscriber;
            return input.CreateMethodReturn(null);
        }

        #endregion Private Methods 

        #endregion Methods 
    }
}
