using System;
using System.Linq;
using Cellent.Template.Client.Core.Core;
using Cellent.Template.Common.Constants;
using Prism.Commands;

namespace Cellent.Template.Client.Core.Commands
{
    /// <summary>
    /// Generische Version des Cellent Commands
    /// </summary>
    public class CellentCommand<T> : DelegateCommand<T>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CellentCommand" /> class.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        public CellentCommand(Action<T> executeMethod) : base(executeMethod)
        {
            IsAvailable = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellentCommand" /> class.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        public CellentCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
            IsAvailable = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellentCommand" /> class.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="right">The right.</param>
        public CellentCommand(Action<T> executeMethod, ClientRights right) : base(executeMethod, arg => ClientContext.ClientRights.Contains(right))
        {
            IsAvailable = ClientContext.ClientRights.Contains(right);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellentCommand" /> class.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute methhod.</param>
        /// <param name="right">The right.</param>
        public CellentCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod, ClientRights right) 
            : base(executeMethod, arg =>
            {
                //hm. ist das elegant? Vielleicht eine passendere Basiklasse suchen.
                bool isAvailable = ClientContext.ClientRights.Contains(right);
                if (!isAvailable)
                    return false;
                return canExecuteMethod.Invoke(arg);
            })
        {
            IsAvailable = ClientContext.ClientRights.Contains(right);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is available.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is available; otherwise, <c>false</c>.
        /// </value>
        public bool IsAvailable { get; private set; }

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns></returns>
        public override bool CanExecute(T parameter)
        {
            return base.CanExecute(parameter) && IsAvailable;
        }
    }
}
