using Cellent.Template.Domain.Core.Implementations;
using Microsoft.Practices.Unity;

namespace Cellent.Template.Domain.MasterDataModule
{
    /// <summary>
    /// UserModule
    /// </summary>
    public class Module : BaseModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Module"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public Module(IUnityContainer container) : base(container)
        {
        }
    }
}