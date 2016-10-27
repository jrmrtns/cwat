using System;
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
    public class LoggingInterceptor : ICallHandler
    {
        #region Properties (2) 

        /// <summary>
        /// Order in which the handler will be executed
        /// </summary>
        public int Order { get; set; }

        #endregion Properties 

        #region Methods (1) 

        #region Public Methods (1) 

        /// <summary>
        /// Implement this method to execute your handler processing.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the handler
        /// chain.</param>
        /// <returns>
        /// Return value from the target.
        /// </returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            string name = "unknown User";
            try
            {
                string n = Thread.CurrentPrincipal.Identity.Name;
                if (string.IsNullOrEmpty(n))
                    name = null;
            }
            catch (ObjectDisposedException)
            {
                //kann passieren, dann steht weiter unknown user im log
            }

            string param = input.Arguments.Cast<object>()
                .Aggregate("", (current, parameter) => current + (", " + (parameter == null ? "null" : parameter.ToString())));

            if (param.Length > 0) param = param.Substring(2);

            Logger.Logger.Write(string.Format("{0}: Enter Method: {1}.{2}({3})", name, input.MethodBase.DeclaringType, input.MethodBase.Name, param), TraceEventType.Verbose);

            IMethodReturn value = getNext()(input, getNext);
            value.InvokeAfterCall(input, d =>
            {
                if (d.Exception != null)
                {
                    Logger.Logger.Write(d.Exception);
                }

                Logger.Logger.Write(string.Format("{0}: Exit Method: {1}.{2}({3})", name, input.MethodBase.DeclaringType, input.MethodBase.Name, param), TraceEventType.Verbose);
            });
            return value;
        }

        #endregion Public Methods 

        #endregion Methods 
    }
}
