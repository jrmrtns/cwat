using Cellent.Template.Client.Core.Commands;
using Cellent.Template.Client.Core.Core;
using Cellent.Template.Client.Core.Core.Resources;
using Cellent.Template.Client.Core.Interfaces;
using Cellent.Template.Client.Core.Interfaces.Factories;
using Cellent.Template.Client.Core.Interfaces.Models;
using Cellent.Template.Common.Constants;
using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Interceptors;
using Cellent.Template.Common.Interfaces.WCFServices;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Cellent.Template.Common.ServiceClient;

namespace Cellent.Template.ClientModule.User.ViewModels
{
    /// <summary>
    /// Viewmodel für UserListView
    /// </summary>
    [Log]
    public class UserListViewModel : BaseViewModel
    {
        #region Private Fields

        private readonly CellentCommand _addUserCommand;
        private readonly InteractionRequest<UserDialogNotification> _addUserInteractionRequest;
        private readonly ICommand _doubleClikOnUserCommand;
        private readonly CellentCommand _editUserCommand;
        private readonly CellentCommand<object> _editUserCommandParameter;
        private readonly INotificationService _notificationService;
        private readonly CellentCommand _refreshListCommand;
        private readonly IRegionManager _regionManager;
        private readonly IModelFactory<IUserModel, UserDto> _userFactory;
        private IUserModel _selectedUser;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserListViewModel" /> class.
        /// </summary>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="userFactory">The user mapper.</param>
        public UserListViewModel(INotificationService notificationService, IRegionManager regionManager, IModelFactory<IUserModel, UserDto> userFactory)
        {
            _notificationService = notificationService;
            _regionManager = regionManager;
            _userFactory = userFactory;
            _doubleClikOnUserCommand = new CellentCommand(ExecuteEditUser);
            _editUserCommand = new CellentCommand(ExecuteEditUser, CanExecuteEditUser);
            _addUserCommand = new CellentCommand(ExecuteAddUser, ClientRights.CanWriteUser);
            _refreshListCommand = new CellentCommand(LoadData);
            _editUserCommandParameter = new CellentCommand<object>(p => ExecuteEditUser(p));

            _addUserInteractionRequest = new InteractionRequest<UserDialogNotification>();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the add user command.
        /// </summary>
        /// <value>
        /// The add user command.
        /// </value>
        public CellentCommand AddUserCommand
        {
            get { return _addUserCommand; }
        }

        /// <summary>
        /// Gets the add user interaction request.
        /// </summary>
        /// <value>
        /// The add user interaction request.
        /// </value>
        public InteractionRequest<UserDialogNotification> AddUserInteractionRequest
        {
            get { return _addUserInteractionRequest; }
        }

        /// <summary>
        /// Gets or sets AreUsersLoading
        /// </summary>
        public virtual bool AreUsersLoading { get; set; }

        /// <summary>
        /// Gets the DoubleClikOnUser command.
        /// </summary>
        public ICommand DoubleClikOnUserCommand
        {
            get { return _doubleClikOnUserCommand; }
        }

        /// <summary>
        /// Gets the edit user command.
        /// </summary>
        /// <value>
        /// The edit user command.
        /// </value>
        public CellentCommand EditUserCommand
        {
            get { return _editUserCommand; }
        }

        /// <summary>
        /// Edit user command mit Parameter
        /// </summary>
        public CellentCommand<object> EditUserCommandParameter
        {
            get { return _editUserCommandParameter; }
        }

        /// <summary>
        /// Gets the notification service.
        /// </summary>
        /// <value>
        /// The notification service.
        /// </value>
        public INotificationService NotificationService
        {
            get { return _notificationService; }
        }

        /// <summary>
        /// Gets the refresh list command
        /// </summary>
        public CellentCommand RefreshCommand
        {
            get { return _refreshListCommand; }
        }

        /// <summary>
        /// Gets or sets the selected user.
        /// </summary>
        /// <value>
        /// The selected user.
        /// </value>
        public virtual IUserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                EditUserCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets the user mapper.
        /// </summary>
        /// <value>
        /// The user mapper.
        /// </value>
        public IModelFactory<IUserModel, UserDto> UserFactory
        {
            get { return _userFactory; }
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual ListCollectionView Users { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Determines whether [is navigation target] [the specified navigation context].
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns></returns>
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// Initialisiert die View
        /// </summary>
        public async void LoadData()
        {
            try
            {
                AreUsersLoading = true;

                IEnumerable<IUserModel> users = UserFactory
                    .Convert(await ServiceClient<IUserService>.ExecuteAsync(d => d.FindUsersAsync()))
                    .ToList();

                Users = new ListCollectionView(users.ToList());
            }
            catch (Exception ex)
            {
                NotificationService.ShowDialog(ex.Message, Translation.Translate("Error"));
            }
            finally
            {
                AreUsersLoading = false;
            }
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Executes the add user command.
        /// </summary>
        protected virtual void ExecuteAddUser()
        {
            OpenEditDialog(null);
        }

        /// <summary>
        /// Executes the edit user command.
        /// </summary>
        protected virtual void ExecuteEditUser()
        {
            if (SelectedUser != null)
                OpenEditDialog(SelectedUser);
        }

        /// <summary>
        /// Executes the edit user command.
        /// </summary>
        protected virtual void ExecuteEditUser(object user)
        {
            SelectedUser = user as IUserModel;
            OpenEditDialog(SelectedUser);
        }

        private void OpenEditDialog(IUserModel user)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(NavigationParameterNames.User, user ?? _userFactory.Create());
            _regionManager.RequestNavigate(Constants.Regions.MainRegion, new Uri(Constants.ViewNames.CreateUserView, UriKind.Relative), parameters);
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Can execute edit user command
        /// </summary>
        /// <returns></returns>
        protected bool CanExecuteEditUser()
        {
            return SelectedUser != null;
        }

        #endregion Private Methods
    }
}