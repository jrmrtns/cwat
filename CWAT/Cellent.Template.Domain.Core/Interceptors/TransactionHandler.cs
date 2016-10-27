using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;

namespace Cellent.Template.Domain.Core.Interceptors
{
    /// <summary>
    /// Handler um mehrere Statements in einer Transaktion abzuhandeln
    /// </summary>
    public class TransactionHandler : IInterceptionBehavior
    {
        /// <summary>
        /// Order in which the handler will be executed
        /// </summary>
        public int Order { get; set; }

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
            if (input.MethodBase.CustomAttributes.All(d => d.AttributeType != typeof(TransactionAttribute)))
                return getNext()(input, getNext);

            IMethodReturn value;

            TransactionOptions transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = IsolationLevel.ReadCommitted;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                value = getNext()(input, getNext);

                var returnType = (input.MethodBase as MethodInfo).ReturnType;
                if (returnType == typeof(Task) || (returnType.IsGenericType && returnType.BaseType == typeof(Task)))
                {
                    Task t = Task.Run(async () => await CreateWrapperTask(value.ReturnValue as Task, input));
                    t.Wait();
                }

                scope.Complete();
            }
            return value;
        }

        private async Task CreateWrapperTask(Task task, IMethodInvocation input)
        {
            try
            {
                await task.ConfigureAwait(false);
                Trace.TraceInformation("Successfully finished async operation {0}", input.MethodBase.Name);
            }
            catch (Exception e)
            {
                Trace.TraceWarning("Async operation {0} threw: {1}", input.MethodBase.Name, e);
                throw;
            }
        }

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
        /// Returns a flag indicating if this behavior will actually do anything when invoked.
        /// </summary>
        /// <remarks>
        /// This is used to optimize interception. If the behaviors won't actually
        /// do anything (for example, PIAB where no policies match) then the interception
        /// mechanism can be skipped completely.
        /// </remarks>
        public bool WillExecute { get { return true; } }
    }
}