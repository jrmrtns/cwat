using Cellent.Template.Common.Constants;

namespace Cellent.Template.Client
{
    /// <summary>
    /// Result of the load clientcontext
    /// </summary>
    public class LoadClientResult
    {

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadClientResult"/> class.
        /// </summary>
        public LoadClientResult()
        {
            FaultId = Constants.FaultExceptionEnum.Default;
        }
        #endregion Constructor

        #region Properties
        /// <summary>
        /// Gets or sets the result
        /// </summary>
        public bool IsSuccessfull { get; set; }

        /// <summary>
        /// Gets or sets the Faultexception ID
        /// </summary>
        public Constants.FaultExceptionEnum  FaultId {get; set;}
        #endregion Properties
    }
}
