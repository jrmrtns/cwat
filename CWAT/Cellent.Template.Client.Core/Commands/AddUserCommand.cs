using System;

namespace DCX.DPE.Client.Core.Commands
{
    /// <summary>
    /// AdministrationCommand
    /// </summary>
    public class AddUserCommand : BaseCommand
    {
        private readonly Action _executeMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddUserCommand" /> class.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecute">if set to <c>true</c> [can execute].</param>
        public AddUserCommand(Action executeMethod, bool canExecute)
            :base(canExecute)
        {
            _executeMethod = executeMethod;
        }

        /// <summary>
        /// führt das Command aus
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            _executeMethod.Invoke();
        }
    }
}
