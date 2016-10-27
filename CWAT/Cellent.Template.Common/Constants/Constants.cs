using System;

namespace Cellent.Template.Common.Constants
{
    /// <summary>
    /// Alle Konstanten werden hier eingetragen
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Gets the guid of the admin
        /// </summary>
        public static Guid AdminGuid = Guid.Parse("310357DE-D49D-4FD6-B954-656C88D40BBD");

        /// <summary>
        /// The guest unique identifier
        /// </summary>
        public static Guid GuestGuid = Guid.Parse("6DDCFB91-16F3-45F7-8BDE-6C7B9E3BA646");

        /// <summary>
        /// Welche Buttons sollen im Dialog angezeigt werden?
        /// </summary>
        public enum ConfirmationButtons
        {
            /// <summary>
            /// The ok
            /// </summary>
            Ok,

            /// <summary>
            /// The ok cancel
            /// </summary>
            OkCancel,

            /// <summary>
            /// The yes
            /// </summary>
            Yes,

            /// <summary>
            /// The yes no
            /// </summary>
            YesNo
        };

        /// <summary>
        /// States for the Entities
        /// </summary>
        public enum EntityState
        {
            /// <summary>
            /// The created state
            /// </summary>
            Created,

            /// <summary>
            /// The loaded state
            /// </summary>
            Unchanged,

            /// <summary>
            /// The modified state
            /// </summary>
            Modified,

            /// <summary>
            /// The deleted state
            /// </summary>
            Deleted
        }

        /// <summary>
        /// Enum for Faultexception typ
        /// </summary>
        public enum FaultExceptionEnum
        {
            /// <summary>
            /// The default
            /// </summary>
            Default = 0,

            /// <summary>
            /// The unknow user
            /// </summary>
            UnknowUser,

            /// <summary>
            /// The deactived user
            /// </summary>
            DeactivedUser
        }

        /// <summary>
        /// Gets or sets the on behalf user.
        /// </summary>
        /// <value>
        /// The on behalf user.
        /// </value>
        public static string OnBehalfUser { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public static Guid UserId { get; set; }

        /// <summary>
        /// Konstanten für Module
        /// </summary>
        public static class Modules
        {
            /// <summary>
            /// The resource module
            /// </summary>
            public const string ResourceModule = "ResourceModule";

            /// <summary>
            /// The user module
            /// </summary>
            public const string UserModule = "UserModule";
        }

        /// <summary>
        /// Stringkonstanten für Regions
        /// </summary>
        public static class Regions
        {
            /// <summary>
            /// The main region
            /// </summary>
            public const string MainRegion = "MainRegion";
        }

        /// <summary>
        /// HIer werden die Urls eingetragen
        /// </summary>
        public static class Urls
        {
            /// <summary>
            /// The endpoint configuration name
            /// </summary>
            public const string EndpointConfigurationName = "*";
        }

        /// <summary>
        /// Konstanten für die Namen der Views
        /// </summary>
        public class ViewNames
        {
            /// <summary>
            /// The create user view
            /// </summary>
            public const string CreateUserView = "CreateUserView";

            /// <summary>
            /// The create resource
            /// </summary>
            public static string CreateResource = "CreateResource";

            /// <summary>
            /// The user list
            /// </summary>
            public static string ListUser = "ListUser";

            /// <summary>
            /// The resource list
            /// </summary>
            public static string ResourceList = "ResourceList";
        }
    }
}