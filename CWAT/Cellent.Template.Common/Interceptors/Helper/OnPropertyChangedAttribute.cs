using System;

namespace Cellent.Template.Common.Interceptors.Helper
{
    /// <summary>
    /// Feuert zusätzlich NPC-Events
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class OnPropertyChangedAttribute : Attribute
    {
        private readonly string _propertyName;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnPropertyChangedAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public OnPropertyChangedAttribute(String propertyName)
        {
            _propertyName = propertyName;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        public string PropertyName
        {
            get { return _propertyName; }
        }
    }
}
