using Cellent.Template.Common.Constants;
using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Exceptions;
using Cellent.Template.Common.Interfaces.Core;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Cellent.Template.Client.Core.Core
{
    /// <summary>
    /// Basis für alle Models
    /// </summary>
    public class BaseModel : IBaseModel
    {
        #region Methods (2) 

        #region Public Methods (1) 

        /// <summary>
        /// Registriert die Überwachung des States
        /// </summary>
        /// <exception cref="InvalidCreationMethod"></exception>
        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public void RegisterStateTracking()
        {
            INotifyPropertyChanged change = this as INotifyPropertyChanged;
            if (change == null)
                throw new InvalidCreationMethod();

            change.PropertyChanged += BaseModelPropertyChanged;
            if (this is IBindable)
                ((IBindable)this).IsChangeNotificationActive = true;
        }

        #endregion Public Methods 

        #region Private Methods (1) 

        private void BaseModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Nach Deserialisierung im WCF ist der State Created. Changetracking beginnt erst wenn der Status Unchanged ist.
            if (e.PropertyName != "State" && e.PropertyName != "IsValid" && State == Constants.EntityState.Unchanged)
                State = Constants.EntityState.Modified;
        }

        #endregion Private Methods 

        #endregion Methods 

        #region Properties (7) 

        /// <summary>
        /// Gets or sets the changed at.
        /// </summary>
        /// <value>
        /// The changed at.
        /// </value>
        public virtual DateTime ChangedAt { get; set; }

        /// <summary>
        /// Gets or sets the changed by.
        /// </summary>
        /// <value>
        /// The changed by.
        /// </value>
        public virtual Guid ChangedBy { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public virtual DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public virtual Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public Constants.EntityState State { get; set; }

        #endregion Properties

        #region Methods (1) 

        #region Public Methods (1) 

        /// <summary>
        /// Maps the base fields.
        /// </summary>
        /// <param name="source">The source.</param>
        public void MapBaseFields(BaseDto source)
        {
            Id = source.Id;
            CreatedAt = source.CreatedAt;
            CreatedBy = source.CreatedBy;
            ChangedAt = source.ChangedAt;
            ChangedBy = source.ChangedBy;
            State = source.State;
            if (State == Constants.EntityState.Unchanged)
                IsValid = true;
        }

        #endregion Public Methods 

        #endregion Methods
    }
}