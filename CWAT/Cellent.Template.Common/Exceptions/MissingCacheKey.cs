using System;

namespace Cellent.Template.Common.Exceptions
{
    /// <summary>
    /// Wird geworfen wenn ein zu cachender Eintrag keinen Key besitzt oder sich aus Parametern kein Key erzeugen lässt
    /// </summary>
    [Serializable]
    public class MissingCacheKey : Exception
    {
    }
}
