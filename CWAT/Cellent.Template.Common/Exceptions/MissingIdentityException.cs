using System;
using System.Runtime.Serialization;
using System.Security;

namespace Cellent.Template.Common.Exceptions
{
    /// <summary>
    /// Wird geworfen, wenn die Identität des Aufrufers nicht gefunden werden kann
    /// </summary>
    [Serializable]
    public class MissingIdentityException : Exception
    {
        /// <summary>
        /// Konstruktor aus der Basisklasse
        /// </summary>
        /// <param name="serializationInfo"></param>
        /// <param name="streamingContext"></param>
        [SecuritySafeCritical]
        public MissingIdentityException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            :base(serializationInfo, streamingContext)
        {}

        /// <summary>
        /// Konstruktor aus der Basisklasse
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public MissingIdentityException(string message, Exception exception)
            :base(message, exception)
        {}

        /// <summary>
        /// Konstruktor aus der Basisklasse
        /// </summary>
        /// <param name="message"></param>
        public MissingIdentityException(string message)
            :base(message)
        {}

        /// <summary>
        /// Konstruktor aus der Basisklasse
        /// </summary>
        public MissingIdentityException()
        {}
    }
}