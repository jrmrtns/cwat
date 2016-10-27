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
using Cellent.Template.Domain.Core.Interfaces.Entities;
using Cellent.Template.Domain.Core.Interfaces.Factories;
using Cellent.Template.Domain.Core.Entities;

namespace Cellent.Template.Domain.MasterDataModule.DaoMapper
{
	/// <summary>
	/// Mapper für ChangeLog
	/// </summary>
	partial class ChangeLogMapper : GenericDaoMapper<IChangeLog, ChangeLogDao>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ChangeLogMapper"/> class.
		/// </summary>
		/// <param name="factory">The factory.</param>
		public ChangeLogMapper(Lazy<IDomainFactory<IChangeLog>> factory) 
			: base(factory)
		{}

		partial void OnConvertAdditionalFields(IChangeLog source, ChangeLogDao dest);
		partial void OnConvertAdditionalFields(ChangeLogDao source,  IChangeLog dest);

		/// <summary>
		/// Converts the specified source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public override ChangeLogDao Convert(IChangeLog source)
		{
			if (source == null)
				return null;

			ChangeLogDao dest = new ChangeLogDao();

			dest.ObjectId = source.ObjectId;
			dest.OldValue = source.OldValue;
			dest.NewValue = source.NewValue;
			dest.Property = source.Property;
			OnConvertAdditionalFields(source, dest);
			dest.MapBaseFields(source);

			return dest;
		}

		/// <summary>
		/// Converts the specified source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public override IChangeLog Convert(ChangeLogDao source)
		{
			if (source == null)
				return null;

			IChangeLog dest = Factory.Create();

			dest.ObjectId = source.ObjectId;
			dest.OldValue = source.OldValue;
			dest.NewValue = source.NewValue;
			dest.Property = source.Property;
			OnConvertAdditionalFields(source, dest);
			dest.MapBaseFields(source);

			return dest;
		}
	}
}
#endregion
