using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Interceptors;
using Cellent.Template.Common.Interfaces.WCFServices;
using Cellent.Template.Domain.Core.Implementations;
using Cellent.Template.Domain.Core.Interfaces.Factories;
using Cellent.Template.Domain.Interfaces.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Cellent.Template.Service.Services
{
    /// <summary>
    /// Implementierung des IService
    /// </summary>
    [Log]
    public class ResourceService : IResourceService
    {
        private readonly IDomainFactory<IResource> _resourceFactory;
        private readonly GenericDomainMapper<IResource, ResourceDto> _resourceMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceService"/> class.
        /// </summary>
        /// <param name="resourceMapper">the generic mapper</param>
        /// <param name="resourceFactory"></param>
        public ResourceService(GenericDomainMapper<IResource, ResourceDto> resourceMapper, IDomainFactory<IResource> resourceFactory)
        {
            _resourceMapper = resourceMapper;
            _resourceFactory = resourceFactory;
        }

        /// <summary>
        /// Löscht eine Ressource
        /// </summary>
        /// <param name="resourceDto"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(ResourceDto resourceDto)
        {
            IResource resource = _resourceMapper.Convert(resourceDto);
            return await resource.DeleteAsync(resource);
        }

        /// <summary>
        /// Finds the resources.
        /// </summary>
        /// <returns>
        /// IEnumerable of the ResourceDto
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual async Task<IEnumerable<ResourceDto>> FindAsync()
        {
            IResource resource = _resourceFactory.Create();
            return _resourceMapper.Convert(await resource.FindAllAsync());
        }

        /// <summary>
        /// Finds the resources.
        /// </summary>
        /// <param name="cultureInfo">The cultureInfo.</param>
        /// <returns>
        /// IEnumerable of the ResourceDto
        /// </returns>
        public virtual IEnumerable<ResourceDto> FindForCultureInfo(CultureInfo cultureInfo)
        {
            IResource resource = _resourceFactory.Create();
            return _resourceMapper.Convert(resource.FindForCultureInfo(cultureInfo));
        }

        /// <summary>
        /// Adds a new resources or updates existing.
        /// </summary>
        /// <param name="resourceDto">The resource.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual async Task<ResourceDto> SaveAsync(ResourceDto resourceDto)
        {
            IResource resource = _resourceMapper.Convert(resourceDto);
            return _resourceMapper.Convert(await resource.SaveOrUpdateAsync(resource));
        }
    }
}