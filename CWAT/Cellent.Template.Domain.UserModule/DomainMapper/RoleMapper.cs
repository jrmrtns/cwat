using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Domain.Core.Implementations;
using Cellent.Template.Domain.Core.Interfaces.Factories;
using Cellent.Template.Domain.Interfaces.Entities;

namespace Cellent.Template.Domain.UserModule.DomainMapper
{
    public partial class RoleMapper
    {
        private readonly GenericDomainMapper<IRight, RightDto> _rightMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleMapper"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="rightMapper">The right mapper.</param>
        public RoleMapper(IDomainFactory<IRole> factory, GenericDomainMapper<IRight, RightDto> rightMapper)
            : this(factory)
        {
            _rightMapper = rightMapper;
        }

        partial void OnConvertAdditionalFields(IRole source, RoleDto dest)
        {
            if (source.Rights != null)
                dest.Rights = _rightMapper.Convert(source.Rights);
        }

        partial void OnConvertAdditionalFields(RoleDto source, IRole dest)
        {
            if (source.Rights != null)
                dest.Rights = _rightMapper.Convert(source.Rights);
        }
    }
}