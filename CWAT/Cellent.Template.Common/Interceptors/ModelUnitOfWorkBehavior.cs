using System;
using System.Collections.Generic;
using System.Linq;
using Cellent.Template.Common.Interceptors.Helper;
using Cellent.Template.Common.Interfaces.Core;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Cellent.Template.Common.Interceptors
{
    /// <summary>
    /// UnitOfWorkBehavior
    /// </summary>
    public class ModelUnitOfWorkBehavior : IInterceptionBehavior
    {
        private readonly IDictionary<string, UnitOfWorkItem> _changeLog = new Dictionary<string, UnitOfWorkItem>(); 

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

        /// <summary>
        /// Gets the change log.
        /// </summary>
        /// <value>
        /// The change log.
        /// </value>
        private IDictionary<string, UnitOfWorkItem> ChangeLog
        {
            get { return _changeLog; }
        }

        #endregion Properties 

        #region Methods (3) 

        #region Public Methods (2) 

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
        /// Implement this method to execute your behavior processing.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the behavior chain.</param>
        /// <returns>
        /// Return value from the target.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (input.MethodBase.Name == "get_ChangeLog" && input.MethodBase.DeclaringType == typeof(IUnitOfWork))
                return input.CreateMethodReturn(ChangeLog.Values.ToArray());

            if (input.MethodBase.Name == "AddChange" && input.MethodBase.DeclaringType == typeof (IUnitOfWork))
            {
                UnitOfWorkItem unitOfWorkItem = input.Arguments[0] as UnitOfWorkItem;
                if (!_changeLog.ContainsKey(unitOfWorkItem.PropertyName))
                    _changeLog.Add(unitOfWorkItem.PropertyName, unitOfWorkItem);

                return input.CreateMethodReturn(null);
            }

            IBaseModel model = input.Target as IBaseModel;

            if (IsRelevantProperty(input) && (model.State == Constants.Constants.EntityState.Unchanged || model.State == Constants.Constants.EntityState.Modified))
            {
                if (model.State == Constants.Constants.EntityState.Unchanged)
                    _changeLog.Clear();

                string propertyName = input.MethodBase.Name.Substring(4);

                if (_changeLog.ContainsKey(propertyName))
                {
                    var item = _changeLog[propertyName];
                    item.NewValue = input.Arguments[0] == null ? null : input.Arguments[0].ToString();
                    item.TimeStamp = DateTime.Now;
                }
                else
                {
                    object oldValue = input.Target.GetType().GetProperty(propertyName).GetValue(input.Target);
                    if (oldValue == null && input.Arguments[0] == null)
                        return getNext()(input, getNext);

                    if (oldValue != null && oldValue.Equals(input.Arguments[0]))
                        return getNext()(input, getNext);

                    UnitOfWorkItem item = new UnitOfWorkItem
                    {
                        OldVaue = oldValue == null ? null : oldValue.ToString(),
                        NewValue = input.Arguments[0] == null ? null : input.Arguments[0].ToString(),
                        TimeStamp = DateTime.Now,
                        PropertyName = propertyName,
                        ObjectId = model.Id,
                        DomainObject = input.MethodBase.DeclaringType?.Name,
                        ChangedBy = Constants.Constants.UserId
                    };
                    _changeLog.Add(propertyName, item);
                }
            }

            return getNext()(input, getNext);
        }

        #endregion Public Methods 
        
        #region Private Methods (1) 

        private static bool IsRelevantProperty(IMethodInvocation input)
        {
            return input.MethodBase.IsSpecialName 
                && input.MethodBase.Name.StartsWith("set_") 
                && !input.MethodBase.Name.StartsWith("set_State") 
                && !input.MethodBase.Name.StartsWith("set_IsValid");
        }

        #endregion Private Methods 

        #endregion Methods 
    }
}
