using System.Threading.Tasks;

namespace Cellent.Template.Client.Core.Interfaces.Models
{
    /// <summary>
    /// Interface für User
    /// </summary>
    public partial interface IResourceModel
    {
        /// <summary>
        /// Saves this instance.
        /// </summary>
        Task<IResourceModel> SaveAsync();
    }
}
