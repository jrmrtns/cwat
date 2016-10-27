using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Exceptions;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Cellent.Template.Common.Interfaces.WCFServices
{
    /// <summary>
    /// Test WcfService
    /// </summary>
    [ServiceContract(Namespace = "http://schemas.daimler.com/Cellent")]
    public interface IResourceService
    {
        /// <summary>
        /// Finds the resources.
        /// </summary>
        /// <returns>IEnumerable of the ResourceDto</returns>
        [OperationContract]
        [FaultContract(typeof(RemoteFault))]
        Task<IEnumerable<ResourceDto>> FindAsync();

        /// <summary>
        /// Finds the resources.
        /// </summary>
        /// <param name="cultureInfo">The cultureInfo.</param>
        /// <returns>IEnumerable of the ResourceDto</returns>
        [OperationContract]
        [FaultContract(typeof(RemoteFault))]
        [ServiceKnownType(typeof(CultureInfo))]
        [ServiceKnownType(typeof(GregorianCalendar))]
        IEnumerable<ResourceDto> FindForCultureInfo(CultureInfo cultureInfo);

        /// <summary>
        /// Adds a new resources or updates existing.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(RemoteFault))]
        Task<ResourceDto> SaveAsync(ResourceDto resource);

        /// <summary>
        /// Löscht eine Ressource
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(RemoteFault))]
        Task<bool> DeleteAsync(ResourceDto resource);
    }
}