using Cellent.Template.Domain.Interfaces.Entities;
using Cellent.Template.Repository.Interfaces.Repositories;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Cellent.Template.Domain.ResourceModule.Entities
{
    public partial class Resource
    {
        public async Task<bool> DeleteAsync(IResource resource)
        {
            return await Repository.DeleteAsync(resource);
        }

        public IEnumerable<IResource> FindForCultureInfo(CultureInfo cultureInfo)
        {
            return ((IResourceRepository)Repository).FindForCultureInfo(cultureInfo);
        }

        public async Task<IEnumerable<IResource>> FindAllAsync()
        {
            return await ((IResourceRepository)Repository).FindAllAsync();
        }
    }
}