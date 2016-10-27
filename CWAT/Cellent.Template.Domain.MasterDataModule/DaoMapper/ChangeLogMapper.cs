using Cellent.Template.Domain.Core;
using Cellent.Template.Domain.Core.Entities;
using Cellent.Template.Domain.Core.Interfaces.Entities;
using Cellent.Template.Domain.Core.Interfaces.Factories;
using Cellent.Template.Domain.Core.Interfaces.Mapper;
using System;

namespace Cellent.Template.Domain.MasterDataModule.DaoMapper
{
    partial class ChangeLogMapper
    {
        private readonly IGenericDaoMapper<IDomainObject, DomainObjectDao> _domainObjectMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeLogMapper"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="domainObjectMapper">The domain object mapper.</param>
        public ChangeLogMapper(Lazy<IDomainFactory<IChangeLog>> factory, GenericDaoMapper<IDomainObject, DomainObjectDao> domainObjectMapper) : this(factory)
        {
            _domainObjectMapper = domainObjectMapper;
        }

        partial void OnConvertAdditionalFields(IChangeLog source, ChangeLogDao dest)
        {
            dest.DomainObject = _domainObjectMapper.Convert(source.DomainObject);
        }

        partial void OnConvertAdditionalFields(ChangeLogDao source, IChangeLog dest)
        {
            dest.DomainObject = _domainObjectMapper.Convert(source.DomainObject);
        }
    }
}