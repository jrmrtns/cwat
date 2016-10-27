using System;

namespace Cellent.Template.Common.Exceptions
{
    /// <summary>
    /// Wird geworfen wenn ein Model nicht über die Factories erzeugt wurde
    /// </summary>
    [Serializable]
    public class InvalidCreationMethod : Exception
    {
    }
}
