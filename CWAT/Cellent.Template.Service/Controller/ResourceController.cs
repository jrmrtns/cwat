using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Interceptors;
using Cellent.Template.Domain.Core.Implementations;
using Cellent.Template.Domain.Core.Interfaces.Factories;
using Cellent.Template.Domain.Interfaces.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Cellent.Template.Service.Controller
{
    [Log]
    public class ResourceController : ApiController
    {
        private readonly GenericDomainMapper<IResource, ResourceDto> _resourceMapper;
        private readonly IDomainFactory<IResource> _resourceFactory;

        public ResourceController(GenericDomainMapper<IResource, ResourceDto> resourceMapper, IDomainFactory<IResource> resourceFactory)
        {
            _resourceMapper = resourceMapper;
            _resourceFactory = resourceFactory;
        }

        [HttpGet]
        public virtual async Task<IEnumerable<ResourceDto>> Get()
        {
            IResource resource = _resourceFactory.Create();
            return _resourceMapper.Convert(await resource.FindAllAsync());
        }
    }
}