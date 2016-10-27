using System;
using System.Windows.Input;

namespace DCX.DPE.Client.Core.Commands
{
    /// <summary>
    /// Basis Command
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        #region Fields
        /// <summary>
        /// The canExecute field
        /// </summary>
        private readonly bool _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommand"/> class.
        /// </summary>
        /// <param name="canExecute">if set to <c>true</c> [can execute].</param>
        protected BaseCommand(bool canExecute)
        {
            _canExecute = canExecute;
        }

        #endregion Fields

        #region Properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        protected String Name { get; set; }

        #endregion Properties

        #region Class extensions - virtual methods

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public virtual event EventHandler CanExecuteChanged;

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public virtual bool CanExecute(object parameter)
        {
            return _canExecute;
        }
        #endregion Class extensions - virtual methods

        #region Private helper methods

        #endregion Private helper methods

        /// <summary>
        /// führt das Command aus
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object parameter);

        /// <summary>
        /// Raises the can execute changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            EventHandler tmp = CanExecuteChanged;
            if (tmp != null)
            {
                tmp(this, EventArgs.Empty);
            }
        }
    }
}
