using System;
using System.Globalization;

namespace Cellent.Template.Client.Core.Core.Resources
{
    /// <summary>
    /// Stores all the resources localized for one particular culture, ignoring all other cultures, including any fallback rules.
    /// </summary>
    public class ResourceSet : System.Resources.ResourceSet
    {
        /// <summary>
        /// Erstellt ein neues ResourceSet
        /// </summary>
        /// <param name="culture"></param>
        public ResourceSet(CultureInfo culture):
            base(new ResourceReader(culture))
        {
        }

        /// <summary>
        /// Returns the preferred resource reader class for this kind of System.Resources.ResourceSet.
        /// </summary>
        /// <returns></returns>
        public override Type GetDefaultReader()
        {
            return typeof(ResourceReader);
        }
    }
}
