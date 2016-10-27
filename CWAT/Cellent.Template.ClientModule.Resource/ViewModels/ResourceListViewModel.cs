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
using Cellent.Template.Common.ServiceClient;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace Cellent.Template.ClientModule.Resource.ViewModels
{
    /// <summary>
    /// Viewmodel für die Listenansicht
    /// </summary>
    [Log]
    public class ResourceListViewModel : BaseViewModel
    {
        #region Fields (7) 

        private readonly IRegionManager _regionManager;
        private readonly INotificationService _notificationService;
        private readonly IModelFactory<IResourceModel, ResourceDto> _resourceFactory;
        private readonly CellentCommand _addResourceCommand;
        private readonly CellentCommand<object> _editResourceCommand;
        private readonly CellentCommand _doubleClickOnResourceCommand;
        private readonly CellentCommand<object> _deleteResourceCommand;
        private readonly CellentCommand _refreshListCommand;

        #endregion Fields 

        #region Constructors (2) 

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceListViewModel" /> class.
        /// </summary>
        /// <param name="resourceFactory">The resource factory.</param>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="regionManager">The region manager.</param>
        public ResourceListViewModel(IModelFactory<IResourceModel, ResourceDto> resourceFactory,
            INotificationService notificationService, IRegionManager regionManager)
        {
            _resourceFactory = resourceFactory;
            _notificationService = notificationService;
            _regionManager = regionManager;
            _addResourceCommand = new CellentCommand(ExecuteCreateResource, ClientRights.CanWriteRessource);
            _editResourceCommand = new CellentCommand<object>(ExecuteEditResource, ClientRights.CanWriteRessource);
            _doubleClickOnResourceCommand = new CellentCommand(ExecuteDoubleClickOnResourceCommand, ClientRights.CanWriteRessource);
            _deleteResourceCommand = new CellentCommand<object>(ExecuteDeleteResource, ClientRights.CanWriteRessource);
            _refreshListCommand = new CellentCommand(LoadData);
        }

        #endregion Constructors 

        #region Properties (5) 

        /// <summary>
        /// Command zum Doppelklick auf Ressource
        /// </summary>
        public CellentCommand DoubleClickOnResourceCommand
        {
            get { return _doubleClickOnResourceCommand; }
        }

        /// <summary>
        /// Gets the refresh list command
        /// </summary>
        public CellentCommand RefreshCommand
        {
            get { return _refreshListCommand; }
        }

        /// <summary>
        /// Gets the add resource command.
        /// </summary>
        /// <value>
        /// The add resource command.
        /// </value>
        public virtual CellentCommand AddResourceCommand
        {
            get { return _addResourceCommand; }
        }

        /// <summary>
        /// Command zum Bearbeiten von einer Ressource
        /// </summary>
        public virtual CellentCommand<object> EditResourceCommand
        {
            get { return _editResourceCommand; }
        }

        /// <summary>
        /// Command zum Löschen von einer Ressource
        /// </summary>
        public CellentCommand<object> DeleteResourceCommand
        {
            get { return _deleteResourceCommand; }
        }

        /// <summary>
        /// Gets or sets the resources.
        /// </summary>
        /// <value>
        /// The resources.
        /// </value>
        public virtual ListCollectionView Resources { get; set; }

        /// <summary>
        /// Gets or sets the selected resource.
        /// </summary>
        /// <value>
        /// The selected resource.
        /// </value>
        public virtual IResourceModel SelectedResource { get; set; }

        /// <summary>
        /// Gets or sets AreUsersLoading
        /// </summary>
        public virtual bool AreResourcesLoading { get; set; }

        #endregion Properties 

        #region Methods (3) 

        #region Protected Methods (1) 

        /// <summary>
        /// Executes the add user command.
        /// </summary>
        public virtual void ExecuteAddResource()
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(NavigationParameterNames.Ressource, SelectedResource ?? _resourceFactory.Create());
            _regionManager.RequestNavigate(Constants.Regions.MainRegion, new Uri(Constants.ViewNames.CreateResource, UriKind.Relative), parameters);
        }

        #endregion Protected Methods 

        #region Private Methods (2) 

        private void ExecuteCreateResource()
        {
            SelectedResource = null;
            ExecuteAddResource();
        }

        private void ExecuteDoubleClickOnResourceCommand()
        {
            if (SelectedResource == null)
                return;
            ExecuteAddResource();
        }

        private void ExecuteEditResource(object obj)
        {
            if (obj != null && obj is IResourceModel)
            {
                SelectedResource = obj as IResourceModel;
                ExecuteAddResource();
            }
        }

        private void ExecuteDeleteResource(object obj)
        {
            _notificationService.ShowDialog(Translation.Translate("ConfirmDelete"), Translation.Translate("Confirmation"),
                async confirmation =>
                {
                    if (confirmation.Confirmed && obj != null && obj is IResourceModel)
                    {
                        SelectedResource = obj as IResourceModel;
                        var factory = ServiceLocator.Current.GetInstance<IModelFactory<IResourceModel, ResourceDto>>();
                        var resDto = factory.Convert(SelectedResource);
                        await ServiceClient<IResourceService>.ExecuteAsync(d => d.DeleteAsync(resDto));
                        LoadData();
                    }
                });
        }

        #endregion Private Methods 

        #endregion Methods 

        /// <summary>
        /// Loads the data.
        /// </summary>
        public async void LoadData()
        {
            try
            {
                AreResourcesLoading = true;

                IEnumerable<IResourceModel> resourceModels = _resourceFactory
                    .Convert(await ServiceClient<IResourceService>.ExecuteAsync(d => d.FindAsync()))
                    .ToList();

                Resources = new ListCollectionView(resourceModels.ToList());
            }
            catch (Exception ex)
            {
                _notificationService.ShowDialog(ex.Message, Translation.Translate("Error"));
            }
            finally
            {
                AreResourcesLoading = false;
            }
        }
    }
}