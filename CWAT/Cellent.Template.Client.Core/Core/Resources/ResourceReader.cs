using Cellent.Template.Common.DataTransferObjects;
using Cellent.Template.Common.Interfaces.WCFServices;
using Cellent.Template.Common.Logger;
using Cellent.Template.Common.ServiceClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Resources;

namespace Cellent.Template.Client.Core.Core.Resources
{
    /// <summary>
    /// Stellt Funktionalität bereit, um aus der Resourcen über den WCF-Service zu lesen
    /// </summary>
    public class ResourceReader : IResourceReader
    {
        #region Fields (2) 

        private readonly CultureInfo _cultureInfo;
        private bool _isDisposed;

        #endregion Fields 

        #region Constructors (2) 

        /// <summary>
        /// Erstellt einen neuene ResourceReader
        /// </summary>
        /// <param name="cultureInfo"></param>
        public ResourceReader(CultureInfo cultureInfo)
        {
            _cultureInfo = cultureInfo;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~ResourceReader()
        {
            Dispose(false);
        }

        #endregion Constructors 

        #region Methods (5) 

        #region Public Methods (3) 

        /// <summary>
        /// Schliest den Reader
        /// </summary>
        public void Close()
        {
        }

        /// <summary>
        /// Gibt verwendete Resourcen wieder frei
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gibt eine Auflistung von Resourcen zurück
        /// </summary>
        /// <returns></returns>
        public IDictionaryEnumerator GetEnumerator()
        {
            Hashtable languages = new Hashtable();

            IEnumerable<ResourceDto> resources = ServiceClient<IResourceService>.Execute(d => d.FindForCultureInfo(_cultureInfo));

            foreach (ResourceDto resource in resources)
            {
                if (languages.ContainsKey(resource.Key.ToLower()))
                {
                    Logger.Write(string.Format("ResourceDto duplicate Key: {0}", resource.Key), TraceEventType.Information);
                    continue;
                }
                languages.Add(resource.Key.ToLower(), resource.Translation);
            }

            return languages.GetEnumerator();
        }

        #endregion Public Methods 

        #region Private Methods (2) 

        /// <summary>
        /// Disposable-Pattern
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                }
            }
            _isDisposed = true;
        }

        /// <summary>
        /// gibt einen Enummerator zurück, der durch die Kollektion iteriert
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion Private Methods 

        #endregion Methods 
    }
}