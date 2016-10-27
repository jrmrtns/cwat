using Cellent.Template.Common.Interceptors.Helper;
using Cellent.Template.Common.Interfaces.Core;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cellent.Template.Domain.Core.Interceptors
{
    /// <summary>
    /// UnitOfWorkBehavior
    /// </summary>
    public class UnitOfWorkBehavior : IInterceptionBehavior
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
            return new[] { typeof(IBaseEntity) };
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
                return input.CreateMethodReturn(ChangeLog.Values);

            if (input.MethodBase.Name == "set_ChangeLog" && input.MethodBase.DeclaringType == typeof(IUnitOfWork))
            {
                IEnumerable<UnitOfWorkItem> items = input.Arguments[0] as IEnumerable<UnitOfWorkItem>;
                ChangeLog.Clear();
                if (items != null)
                {
                    foreach (UnitOfWorkItem item in items)
                    {
                        ChangeLog.Add(item.PropertyName, item);
                    }
                }
                return input.CreateMethodReturn(null);
            }

            IBaseEntity entity = (IBaseEntity)input.Target;

            //Eine Statusänderung interessiert nur wenn das Objekt ungeändert oder verändert ist
            if (entity.State == Common.Constants.Constants.EntityState.Unchanged || entity.State == Common.Constants.Constants.EntityState.Modified)
            {
                if (IsRelevantProperty(input))
                {
                    var propertyName = input.MethodBase.Name.Substring(4);

                    object[] propertyIndex = input
                        .Arguments.Cast<object>()
                        .Where((a, index) => index < input.Arguments.Count - 1)
                        .ToArray();

                    object oldValue = input.Target.GetType().GetProperty(propertyName).GetValue(input.Target, propertyIndex);

                    object newValue = input.Arguments[input.Arguments.Count - 1];

                    if (oldValue != newValue)
                    {
                        input.Target.GetType().GetProperty("State").SetValue(input.Target, Common.Constants.Constants.EntityState.Modified);
                        AddToChangeLog(input, propertyName, entity);
                    }
                }
            }

            return getNext()(input, getNext);
        }

        private void AddToChangeLog(IMethodInvocation input, string propertyName, IBaseEntity entity)
        {
            if (_changeLog.ContainsKey(propertyName))
            {
                var item = _changeLog[propertyName];
                item.NewValue = input.Arguments[0];
                item.TimeStamp = DateTime.Now;
            }
            else
            {
                UnitOfWorkItem item = new UnitOfWorkItem
                {
                    OldVaue = input.Target.GetType().GetProperty(propertyName).GetValue(input.Target),
                    NewValue = input.Arguments[0],
                    TimeStamp = DateTime.Now,
                    PropertyName = propertyName,
                    ObjectId = entity.Id,
                    DomainObject = input.MethodBase.DeclaringType?.Name,
                    ChangedBy = Common.Constants.Constants.UserId
                };
                _changeLog.Add(propertyName, item);
            }
        }

        #endregion Public Methods 

        #region Private Methods (1) 

        private static bool IsRelevantProperty(IMethodInvocation input)
        {
            return input.MethodBase.IsSpecialName && input.MethodBase.Name.StartsWith("set_") && !input.MethodBase.Name.StartsWith("set_State");
        }

        #endregion Private Methods 

        #endregion Methods 
    }
}