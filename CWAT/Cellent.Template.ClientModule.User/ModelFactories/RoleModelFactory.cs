using Cellent.Template.Client.Core.Core;
using Cellent.Template.Client.Core.Interfaces.Models;
using Cellent.Template.Common.DataTransferObjects;
using Prism.Events;

namespace Cellent.Template.ClientModule.User.ModelFactories
{
    public partial class RoleModelFactory
    {
        private GenericFactory<IRightModel, RightDto> _rightFactory;

        /// <summary>
        /// RoleModelFactory
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="rightFactory"></param>
        public RoleModelFactory(IEventAggregator eventAggregator, GenericFactory<IRightModel, RightDto> rightFactory)
            : this(eventAggregator)
        {
            _rightFactory = rightFactory;
        }

        partial void OnConvertAdditionalFields(RoleDto source, IRoleModel dest)
        {
            dest.Rights = _rightFactory.Convert(source.Rights);
        }

        partial void OnConvertAdditionalFields(IRoleModel source, RoleDto dest)
        {
            dest.Rights = _rightFactory.Convert(source.Rights);
        }
    }
}