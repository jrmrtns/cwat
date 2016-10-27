using System;
using System.Runtime.Serialization;

namespace Cellent.Template.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class RemoteFault
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteFault"/> class.
        /// </summary>
        public RemoteFault()
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteFault"/> class.
        /// </summary>
        /// <param name="faultDiscription">The fault discription.</param>
        /// <param name="faultId">The fault identifier.</param>
        public RemoteFault(String faultDiscription, Constants.Constants.FaultExceptionEnum faultId)
        {
            FaultDescription = faultDiscription;
            FaultId = faultId;
        }

        /// <summary>
        /// Gets or sets the fault description.
        /// </summary>
        /// <value>
        /// The fault description.
        /// </value>
        [DataMember]
        public String FaultDescription { get; set; }

        /// <summary>
        /// Gets or sets the fault identifier.
        /// </summary>
        /// <value>
        /// The fault identifier.
        /// </value>
        [DataMember]
        public Constants.Constants.FaultExceptionEnum FaultId { get; set; }
    }
}