using Cellent.Template.Client.Core.Commands;
using Cellent.Template.Client.Core.Core;
using Cellent.Template.Client.Core.Core.Resources;
using Cellent.Template.Client.Core.Interfaces;
using Cellent.Template.Client.Core.Interfaces.Factories;
using Cellent.Template.Client.Core.Interfaces.Models;
using Cellent.Template.Common.Constants;
using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Interceptors;
using Cellent.Template.Common.Interceptors.Helper;
using Cellent.Template.Common.Interfaces.WCFServices;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Cellent.Template.Common.ServiceClient;

namespace Cellent.Template.ClientModule.User.ViewModels
{
    /// <summary>
    /// ViewModel für die CreateUserView
    /// </summary>
    [Log]
    public class CreateUserViewModel : BaseViewModel
    {
        #region Fields

        private readonly ICommand _cancelCommand;
        private readonly IRegionNavigationJournal _journal;
        private readonly INotificationService _notificationService;
        private readonly CellentCommand _okCommand;
        private readonly IModelFactory<IRoleModel, RoleDto> _roleFactory;
        private readonly IModelFactory<IUserModel, UserDto> _userFactory;
        private IUserModel _userModel;
        private ObservableCollection<IRoleModel> _roles;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserViewModel"/> class.
        /// </summary>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="userFactory">The user factory.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="roleFactory">The role factory.</param>
        public CreateUserViewModel(INotificationService notificationService,
            RegionManager regionManager,
            IModelFactory<IUserModel, UserDto> userFactory,
            IModelFactory<IRoleModel, RoleDto> roleFactory)
        {
            _notificationService = notificationService;
            _userFactory = userFactory;
            _roleFactory = roleFactory;
            _cancelCommand = new CellentCommand(ExecuteCancelUser);
            _okCommand = new CellentCommand(SaveUser, CanSaveUser);
            _journal = regionManager.Regions[Constants.Regions.MainRegion].NavigationService.Journal;
        }

        #endregion Constructors

        #region Properties

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
        /// Gets or sets a value indicating whether this instance can edit user.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can edit user; otherwise, <c>false</c>.
        /// </value>
        public virtual bool CanEditUser
        {
            get
            {
                return UserModel != null && (ClientContext.ClientRights.Contains(ClientRights.CanWriteUser)
                    || UserModel.Name.ToLower() == Environment.UserDomainName.ToLower() + "\\" + Environment.UserName.ToLower());
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can edit roles.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can edit user; otherwise, <c>false</c>.
        /// </value>
        public virtual bool CanEditRole
        {
            get { return ClientContext.ClientRights.Contains(ClientRights.CanWriteUser); }
        }

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the value to indicate that a data is been loaded
        /// </summary>
        public virtual bool IsDataLoading { get; set; }

        /// <summary>
        /// Gets or sets IsNameFocused
        /// </summary>
        public virtual bool IsNameFocused { get; set; }

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
        /// Gets or sets the representatives collection view
        /// </summary>
        public virtual ICollectionView Representatives { get; set; }

        /// <summary>
        /// Gets or sets Roles collection view
        /// </summary>
        public virtual ObservableCollection<IRoleModel> Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                if (UserModel != null && UserModel.Role != null)
                    UserModel.Role = Roles.FirstOrDefault(d => d.Id == UserModel.Role.Id);
            }
        }

        /// <summary>
        /// Gets or sets the usermodel
        /// </summary>
        [OnPropertyChanged("CanEditUser")]
        public virtual IUserModel UserModel
        {
            get { return _userModel; }
            set
            {
                _userModel = value;
                if (Roles != null)
                    _userModel.Role = Roles.FirstOrDefault(d => d.Id == _userModel.Role.Id);
                // ReSharper disable once SuspiciousTypeConversion.Global
                ((INotifyPropertyChanged)_userModel).PropertyChanged += CreateUserViewModel_PropertyChanged;
            }
        }

        private void CreateUserViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _okCommand.RaiseCanExecuteChanged();
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// set focus property
        /// </summary>
        public async void LoadData()
        {
            try
            {
                IEnumerable<RoleDto> roles = await ServiceClient<IRoleService>.ExecuteAsync(d => d.FindRolesAsync());
                Roles = new ObservableCollection<IRoleModel>(_roleFactory.Convert(roles));
            }
            catch (Exception ex)
            {
                _notificationService.ShowDialog(ex.Message, Translation.Translate("Error"));
            }

            if (IsNameFocused)
                IsNameFocused = false;

            IsNameFocused = true;
        }

        /// <summary>
        /// Called when [navigated to].
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            IUserModel userModel = navigationContext.Parameters[NavigationParameterNames.User] as IUserModel;
            if (userModel == null)
            {
                userModel = _userFactory.Create();
                userModel.Role = _roleFactory.Create();
            }

            UserModel = userModel;
            _okCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Saves the user.
        /// </summary>
        public virtual async void SaveUser()
        {
            try
            {
                if (UserModel.State != Constants.EntityState.Unchanged)
                {
                    await UserModel.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message, Translation.Translate("Error"));
            }

            NavigateBack();
        }

        /// <summary>
        /// Shows a message dialog
        /// </summary>
        public void ShowDialog(string message, string title)
        {
            _notificationService.ShowDialog(message, title);
        }

        /// <summary>
        /// Execute the cancel user command
        /// </summary>
        protected void ExecuteCancelUser()
        {
            NavigateBack();
        }

        private bool CanSaveUser()
        {
            return UserModel != null && UserModel.IsValid && CanEditUser;
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

        #endregion Methods
    }
}