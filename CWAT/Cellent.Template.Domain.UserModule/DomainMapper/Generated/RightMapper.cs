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
using Cellent.Template.Domain.Interfaces.Entities;
using Cellent.Template.Domain.Core.Interfaces.Factories;

namespace Cellent.Template.Domain.UserModule.DomainMapper
{
    /// <summary>
    /// Mapper für Right
    /// </summary>
    partial class RightMapper : GenericDomainMapper<IRight, RightDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RightMapper"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public RightMapper(IDomainFactory<IRight> factory) 
            : base(factory)
        {}

        partial void OnConvertAdditionalFields(IRight source, RightDto dest);
        partial void OnConvertAdditionalFields(RightDto source,  IRight dest);

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public override RightDto Convert(IRight source)
        {
            if (source == null)
                return null;

            RightDto dest = new RightDto();

            dest.Name = source.Name;
            dest.Description = source.Description;
            dest.Claim = source.Claim;
            dest.Resource = source.Resource;
            OnConvertAdditionalFields(source, dest);
            dest.MapBaseFields(source);

            return dest;
        }

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public override IRight Convert(RightDto source)
        {
            if (source == null)
                return null;

            IRight dest = Factory.Create();

            dest.Name = source.Name;
            dest.Description = source.Description;
            dest.Claim = source.Claim;
            dest.Resource = source.Resource;
            OnConvertAdditionalFields(source, dest);
            dest.MapBaseFields(source);

            return dest;
        }
    }
}

#endregion