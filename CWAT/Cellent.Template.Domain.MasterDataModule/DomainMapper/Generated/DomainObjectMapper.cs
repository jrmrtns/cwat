//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#region T4 generated code

using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Domain.Core.Implementations;
using Cellent.Template.Domain.Core.Interfaces.Entities;
using Cellent.Template.Domain.Core.Interfaces.Factories;

namespace Cellent.Template.Domain.MasterDataModule.DomainMapper
{
    /// <summary>
    /// Mapper für DomainObject
    /// </summary>
    partial class DomainObjectMapper : GenericDomainMapper<IDomainObject, DomainObjectDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainObjectMapper"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public DomainObjectMapper(IDomainFactory<IDomainObject> factory) 
            : base(factory)
        {}

        partial void OnConvertAdditionalFields(IDomainObject source, DomainObjectDto dest);
        partial void OnConvertAdditionalFields(DomainObjectDto source,  IDomainObject dest);

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public override DomainObjectDto Convert(IDomainObject source)
        {
            if (source == null)
                return null;

            DomainObjectDto dest = new DomainObjectDto();

            dest.Type = source.Type;
            dest.Assembly = source.Assembly;
            dest.EntityType = source.EntityType;
            dest.EntityAssembly = source.EntityAssembly;
            dest.DisplayName = source.DisplayName;
            OnConvertAdditionalFields(source, dest);
            dest.MapBaseFields(source);

            return dest;
        }

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public override IDomainObject Convert(DomainObjectDto source)
        {
            if (source == null)
                return null;

            IDomainObject dest = Factory.Create();

            dest.Type = source.Type;
            dest.Assembly = source.Assembly;
            dest.EntityType = source.EntityType;
            dest.EntityAssembly = source.EntityAssembly;
            dest.DisplayName = source.DisplayName;
            OnConvertAdditionalFields(source, dest);
            dest.MapBaseFields(source);

            return dest;
        }
    }
}

#endregion
