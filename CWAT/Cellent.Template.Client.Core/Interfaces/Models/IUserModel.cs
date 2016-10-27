using System.Threading.Tasks;

namespace Cellent.Template.Client.Core.Interfaces.Models
{
    /// <summary>
    /// Interface für User
    /// </summary>
    public partial interface IUserModel
    {
        /// <summary>
        /// Gets or sets Roles
        /// </summary>
        IRoleModel Role { get; set; }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        Task<IUserModel> SaveAsync();
    }
}