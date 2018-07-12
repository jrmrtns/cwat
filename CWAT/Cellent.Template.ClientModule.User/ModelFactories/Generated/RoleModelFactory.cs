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

using System.ComponentModel;
using Cellent.Template.Client.Core.Core;
using Cellent.Template.Client.Core.Interfaces.Models;
using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Interceptors;
using Cellent.Template.Common.Interfaces.Core;
using Microsoft.Practices.Unity.InterceptionExtension;
using Prism.Events;
using Cellent.Template.ClientModule.User.Models;

namespace Cellent.Template.ClientModule.User.ModelFactories
{
    /// <summary>
    /// UserModelFactory
    /// </summary>
    partial class RoleModelFactory : GenericFactory<IRoleModel, RoleDto>
    {
        private readonly IEventAggregator _eventAggregator;
        partial void OnConvertAdditionalFields(IRoleModel source, RoleDto dest);
        partial void OnConvertAdditionalFields(RoleDto source, IRoleModel dest);

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleModelFactory"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public RoleModelFactory(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <returns></returns>
        public override IRoleModel Create()
        {
            return Create(true);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public override IRoleModel Create(bool withChangeNotification)
        {
            var model = Intercept.NewInstanceWithAdditionalInterfaces<RoleModel>(new VirtualMethodInterceptor(),
                new IInterceptionBehavior[]
                    {
                        new DataErrorInfoBehavior(),
                        new NotifyPropertyChangedBehavior(),
                        new ModelUnitOfWorkBehavior()
                    },
                new[]
                {
                    typeof(IDataErrorInfo),
                    typeof(INotifyPropertyChanged),
                    typeof(IBindable),
                    typeof(IUnitOfWork)
                },
                new object[] { _eventAggregator, this });

            if (withChangeNotification)
                model.RegisterStateTracking();
            else
                ((IBindable)model).IsChangeNotificationActive = false;

            return model;
        }

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public override RoleDto Convert(IRoleModel source)
        {
            if (source == null)
                return null;

            RoleDto dest = new RoleDto();

            dest.Name = source.Name;
            dest.Description = source.Description;
            OnConvertAdditionalFields(source, dest);
            dest.MapBaseFields(source);

            return dest;
        }

        /// <summary>
        /// Converts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public override IRoleModel Convert(RoleDto source)
        {
            if (source == null)
                return null;

            IRoleModel dest = Create(false);

            dest.Name = source.Name;
            dest.Description = source.Description;
            OnConvertAdditionalFields(source, dest);
            dest.MapBaseFields(source);

            return dest;
        }
    }
}

#endregion