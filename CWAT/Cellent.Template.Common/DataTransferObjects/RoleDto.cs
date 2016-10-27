using System.Collections.Generic;

namespace Cellent.Template.Common.DataTransferObjects
{
    public partial class RoleDto
    {
        /// <summary>
        /// Gets or sets the right groups.
        /// </summary>
        /// <value>
        /// The right groups.
        /// </value>
        public IEnumerable<RightDto> Rights { get; set; }
    }
}