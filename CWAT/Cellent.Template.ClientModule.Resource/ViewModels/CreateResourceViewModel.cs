using Cellent.Template.Client.Core.Commands;
using Cellent.Template.Client.Core.Core.Resources;
using Cellent.Template.Client.Core.Interfaces;
using Cellent.Template.Client.Core.Interfaces.Models;
using Cellent.Template.Common.Constants;
using Cellent.Template.Common.Interceptors;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Cellent.Template.ClientModule.Resource.ViewModels
{
    /// <summary>
    /// ViewModel für die CreateResourceView
    /// </summary>
    [Log]
    public class CreateResourceViewModel : INavigationAware
    {
        #region Private Fields

        private readonly INotificationService _notificationService;
        private readonly ICommand _cancelCommand;
        private readonly ICommand _okCommand;
        private IResourceModel _resourceModel;
        private readonly IRegionNavigationJournal _journal;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateResourceViewModel" /> class.
        /// </summary>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="regionManager">The region manager.</param>
        public CreateResourceViewModel(INotificationService notificationService, IRegionManager regionManager)
        {
            _notificationService = notificationService;
            _cancelCommand = new CellentCommand(NavigateBack);

            _okCommand = new CellentCommand(Save, () =>
            {
                if (ResourceModel == null)
                    return false;

                return ResourceModel.IsValid;
            });

            _journal = regionManager.Regions[Constants.Regions.MainRegion].NavigationService.Journal;
        }

        /// <summary>
        /// Handles the PropertyChanged event of the CreateResourceViewModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void ResourceModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ((CellentCommand)_okCommand).RaiseCanExecuteChanged();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the Flag indicating if the View is a modal dialog
        /// </summary>
        public bool IsModalDialog { get { return true; } }

        /// <summary>
        /// Gets or sets the resource model.
        /// </summary>
        /// <value>
        /// The resource model.
        /// </value>
        public virtual IResourceModel ResourceModel
        {
            get { return _resourceModel; }
            set
            {
                _resourceModel = value;
                // ReSharper disable once SuspiciousTypeConversion.Global
                ((INotifyPropertyChanged)_resourceModel).PropertyChanged += ResourceModelPropertyChanged;
            }
        }

        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        /// <value>
        /// The cancel command.
        /// </value>
        public virtual ICommand CancelCommand
        {
            get { return _cancelCommand; }
        }

        /// <summary>
        /// Gets the ok command.
        /// </summary>
        /// <value>
        /// The ok command.
        /// </value>
        public virtual ICommand OkCommand
        {
            get { return _okCommand; }
        }

        /// <summary>
        /// Gets the navigate back command.
        /// </summary>
        /// <value>
        /// The navigate back command.
        /// </value>
        public virtual ICommand NavigateBackCommand
        {
            get { return new CellentCommand(NavigateBack); }
        }

        /// <summary>
        /// Gets the navigate forward command.
        /// </summary>
        /// <value>
        /// The navigate forward command.
        /// </value>
        public virtual ICommand NavigateForwardCommand
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>
        /// The languages.
        /// </value>
        public IEnumerable<dynamic> Languages
        {
            get { return new[] { new { Name = "Deutsch", Value = "de-DE" }, new { Name = "Englisch", Value = "en-US" } }; }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Saves the Resource.
        /// </summary>
        public virtual async void Save()
        {
            try
            {
                await ResourceModel.SaveAsync();
                NavigateBack();
            }
            catch (Exception ex)
            {
                _notificationService.ShowDialog(ex.Message, Translation.Translate("Error"));
            }
        }

        #endregion Public Methods

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            IResourceModel userModel = navigationContext.Parameters[NavigationParameterNames.Ressource] as IResourceModel;
            ResourceModel = userModel;
        }

        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns>
        /// <see langword="true" /> if this instance accepts the navigation request; otherwise, <see langword="false" />.
        /// </returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private void NavigateBack()
        {
            if (_journal.CanGoBack)
            {
                _journal.GoBack();
                return;
            }
            throw new InvalidOperationException("NavigationBack cannot be done");
        }
    }
}