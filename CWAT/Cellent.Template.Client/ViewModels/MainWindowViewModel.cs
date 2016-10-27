using Cellent.Template.Client.Core.Core.Resources;
using Cellent.Template.Client.Core.Events;
using Cellent.Template.Client.Core.Interfaces;
using Cellent.Template.Common.Constants;
using Microsoft.Expression.Interactivity.Core;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using System;
using System.Windows.Input;

namespace Cellent.Template.Client.ViewModels
{
    /// <summary>
    /// ViewModel für das MainWindow
    /// </summary>
    public class MainWindowViewModel : INotificationService, IDisposable
    {
        #region Fields

        private readonly InteractionRequest<Confirmation> _confirmationInteractionRequest;
        private readonly ICommand _navigateToResources;
        private readonly SubscriptionToken _notifyUserPubSubEventToken;
        private readonly IRegionManager _regionManager;
        private bool _disposed;
        private IRegionNavigationJournal _journal;
        private IEventAggregator _eventAggregator;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        public MainWindowViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _confirmationInteractionRequest = new InteractionRequest<Confirmation>();

            _notifyUserPubSubEventToken = eventAggregator.GetEvent<NotifyUserPubSubEvent>()
                .Subscribe(d => ShowDialog(d.Message, Translation.Translate("Error")));

            _navigateToResources = new ActionCommand(OnNavigateToResource);
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Finalizes an instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        ~MainWindowViewModel()
        {
            Dispose(false);
        }

        #endregion Destructors

        #region Properties

        /// <summary>
        /// Gets the confirmation interaction request.
        /// </summary>
        /// <value>
        /// The confirmation interaction request.
        /// </value>
        public IInteractionRequest ConfirmationInteractionRequest
        {
            get { return _confirmationInteractionRequest; }
        }

        /// <summary>
        /// Gets or sets the current view.
        /// </summary>
        /// <value>
        /// The current view.
        /// </value>
        public virtual string CurrentView { get; set; }

        /// <summary>
        /// Gets or sets the value indicating that data are been loaded
        /// </summary>
        public virtual bool IsDataLoading { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is menu open.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is menu open; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsMenuOpen { get; set; }

        /// <summary>
        /// Gets the Flag indicating if the View is a modal dialog
        /// </summary>
        public bool IsModalDialog { get { return false; } }

        /// <summary>
        /// Gets the current entry of the journal
        /// </summary>
        public virtual IRegionNavigationJournalEntry JournalCurrentEntry
        {
            get
            {
                if (_journal != null)
                    return _journal.CurrentEntry;

                return null;
            }
        }

        /// <summary>
        /// Gets the NavigateToResources Command.
        /// </summary>
        /// <value>
        /// The navigate to resources.
        /// </value>
        public ICommand NavigateToResources
        {
            get { return _navigateToResources; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            _journal = _regionManager.Regions[Constants.Regions.MainRegion].NavigationService.Journal;
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="title"></param>
        /// <param name="callback">The callback.</param>
        public void ShowDialog(string message, String title, Action<Confirmation> callback)
        {
            _confirmationInteractionRequest.Raise(
                new ConfirmationWrapper
                {
                    Title = title,
                    Content = message,
                    ConfirmationButtons = Constants.ConfirmationButtons.OkCancel
                }, callback);
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="title"></param>
        public void ShowDialog(string message, String title)
        {
            _confirmationInteractionRequest.Raise(
                new ConfirmationWrapper
                {
                    Title = title,
                    Content = message,
                    ConfirmationButtons = Constants.ConfirmationButtons.Ok
                });
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _eventAggregator.GetEvent<NotifyUserPubSubEvent>().Unsubscribe(_notifyUserPubSubEventToken);
                    _notifyUserPubSubEventToken.Dispose();
                }
                _disposed = true;
            }
        }

        private void OnNavigateToResource(object parameter)
        {
            IsMenuOpen = false;

            //Don't navigate to the same view
            if (_journal.CurrentEntry != null && _journal.CurrentEntry.Uri.ToString() == parameter.ToString())
                return;

            Uri target = new Uri(parameter.ToString(), UriKind.Relative);

            _regionManager.RequestNavigate(Constants.Regions.MainRegion, target);
        }

        #endregion Methods

        #region Classes

        /// <summary>
        /// Wrapper um <see cref="Confirmation"/>.
        /// </summary>
        public class ConfirmationWrapper : Confirmation
        {
            #region Properties

            ///  Buttons, die angezeigt werden sollen
            public Constants.ConfirmationButtons ConfirmationButtons { get; set; }

            #endregion Properties
        }

        #endregion Classes
    }
}