using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Cellent.Template.Common.Extensions;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Cellent.Template.Common.Interceptors
{
    /// <summary>
    /// Interceptor für das Log
    /// </summary>
    public class LoggingBehavior : IInterceptionBehavior
    {
        #region Fields (1) 

        private readonly TraceEventType _traceEventType;

        #endregion Fields 

        #region Constructors (1) 

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingBehavior"/> class.
        /// </summary>
        /// <param name="traceEventType">Type of the trace event.</param>
        public LoggingBehavior(TraceEventType traceEventType)
        {
            _traceEventType = traceEventType;
        }

        #endregion Constructors 

        #region Properties (2) 

        /// <summary>
        /// Gets the type of the trace event.
        /// </summary>
        /// <value>
        /// The type of the trace event.
        /// </value>
        public TraceEventType TraceEventType
        {
            get { return _traceEventType; }
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

        #endregion Properties 

        #region Methods (2) 

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
            string name = Thread.CurrentPrincipal.Identity.Name;
            if (string.IsNullOrEmpty(name))
                name = "unknown User";

            string param = input.Arguments.Cast<object>().Aggregate("", (current, parameter) => current + (", " + parameter));
            if (param.Length > 0) param = param.Substring(2);

            Logger.Logger.Write(string.Format("{0}: Enter Method: {1}.{2}({3})", name, input.MethodBase.DeclaringType, input.MethodBase.Name, param), TraceEventType);

            IMethodReturn value = getNext()(input, getNext);
            value.InvokeAfterCall(input, d =>
            {
                if (d.Exception != null)
                {
                    Logger.Logger.Write(d.Exception);
                }

                Logger.Logger.Write(string.Format("{0}: Exit Method: {1}.{2}({3})", name, input.MethodBase.DeclaringType, input.MethodBase.Name, param), TraceEventType);
            });
            return value;
        }

        #endregion Public Methods 

        #endregion Methods 
    }
}
