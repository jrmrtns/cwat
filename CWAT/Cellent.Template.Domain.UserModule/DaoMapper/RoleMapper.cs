using Cellent.Template.Domain.Core;
using Cellent.Template.Domain.Core.Interfaces.Factories;
using Cellent.Template.Domain.Interfaces.Entities;
using Cellent.Template.Repository.Entities;
using System;
using System.Linq;

namespace Cellent.Template.Domain.UserModule.DaoMapper
{
    public partial class RoleMapper
    {
        private readonly GenericDaoMapper<IRight, RightDao> _rightMapper;

        /// <summary>
        /// RoleMapper
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="rightMapper"></param>
        public RoleMapper(Lazy<IDomainFactory<IRole>> factory, GenericDaoMapper<IRight, RightDao> rightMapper)
            : this(factory)
        {
            _rightMapper = rightMapper;
        }

        partial void OnConvertAdditionalFields(RoleDao source, IRole dest)
        {
            if (source.Rights != null)
                dest.Rights = _rightMapper.Convert(source.Rights);
        }

        partial void OnConvertAdditionalFields(IRole source, RoleDao dest)
        {
            if (source.Rights != null)
                dest.Rights = _rightMapper.Convert(source.Rights).ToList();
        }
    }
}