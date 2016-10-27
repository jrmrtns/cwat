using System.Collections;

namespace Cellent.Template.Client.Core.Core.Resources
{
    /// <summary>
    /// Provides convenient access to culture-specific resources at run time
    /// </summary>
    public class ResourceManager : System.Resources.ResourceManager
    {
        private readonly Hashtable _resourceSets;

        #region Constructors (1) 

        /// <summary>
        /// erstellt einen neuen ResourceManager
        /// </summary>
        public ResourceManager()
        {
            _resourceSets = new Hashtable();
        }

        #endregion Constructors 

        #region Methods (1) 

        #region Protected Methods (1) 

        /// <summary>
        /// Provides the implementation for finding a System.Resources.ResourceSet.
        /// </summary>
        /// <param name="culture">The System.Globalization.CultureInfo to look for.</param>
        /// <param name="createIfNotExists"> If true and if the System.Resources.ResourceSet has not been loaded yet, load it</param>
        /// <param name="tryParents">If the System.Resources.ResourceSet cannot be loaded, try parent System.Globalization.CultureInfo objects to see if they exist</param>
        /// <returns>The specified System.Resources.ResourceSet</returns>
        protected override System.Resources.ResourceSet InternalGetResourceSet(System.Globalization.CultureInfo culture, bool createIfNotExists, bool tryParents)
        {
            ResourceSet rs;

            if (_resourceSets.Contains(culture.Name))
            {
                rs = (ResourceSet)_resourceSets[culture.Name];
            }
            else
            {
                rs = new ResourceSet(culture);
                _resourceSets.Add(culture.Name, rs);
            }

            return rs;
           
        }
        #endregion Protected Methods 

        #endregion Methods 
    }
}
