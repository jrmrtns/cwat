using System;

namespace Cellent.Template.Domain.Core.Interceptors
{
    /// <summary>
    /// Handelt die Datenbankaktionen in einer Transaction ab
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class TransactionAttribute : Attribute
    { }
}