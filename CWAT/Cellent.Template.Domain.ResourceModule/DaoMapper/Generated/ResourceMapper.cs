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

using System;
using Cellent.Template.Domain.Core;
using Cellent.Template.Domain.Interfaces.Entities;
using Cellent.Template.Domain.Core.Interfaces.Factories;
using Cellent.Template.Repository.Entities;

namespace Cellent.Template.Domain.ResourceModule.DaoMapper
{
	/// <summary>
	/// Mapper für Resource
	/// </summary>
	partial class ResourceMapper : GenericDaoMapper<IResource, ResourceDao>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ResourceMapper"/> class.
		/// </summary>
		/// <param name="factory">The factory.</param>
		public ResourceMapper(Lazy<IDomainFactory<IResource>> factory) 
			: base(factory)
		{}

		partial void OnConvertAdditionalFields(IResource source, ResourceDao dest);
		partial void OnConvertAdditionalFields(ResourceDao source,  IResource dest);

		/// <summary>
		/// Converts the specified source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public override ResourceDao Convert(IResource source)
		{
			if (source == null)
				return null;

			ResourceDao dest = new ResourceDao();

			dest.Key = source.Key;
			dest.Language = source.Language;
			dest.Description = source.Description;
			dest.Translation = source.Translation;
			OnConvertAdditionalFields(source, dest);
			dest.MapBaseFields(source);

			return dest;
		}

		/// <summary>
		/// Converts the specified source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public override IResource Convert(ResourceDao source)
		{
			if (source == null)
				return null;

			IResource dest = Factory.Create();

			dest.Key = source.Key;
			dest.Language = source.Language;
			dest.Description = source.Description;
			dest.Translation = source.Translation;
			OnConvertAdditionalFields(source, dest);
			dest.MapBaseFields(source);

			return dest;
		}
	}
}
#endregion