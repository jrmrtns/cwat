using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Cellent.Template.Domain.Interfaces.Entities
{
    public partial interface IResource
    {
        Task<IEnumerable<IResource>> FindAllAsync();

        Task<bool> DeleteAsync(IResource resource);

        IEnumerable<IResource> FindForCultureInfo(CultureInfo cultureInfo);
    }
}