using System.Collections.Generic;
using System.Text;

namespace Cellent.Template.Common.Validation
{
    /// <summary>
    /// ValidationSummary
    /// </summary>
    public class ValidationSummary : List<ValidationEntry>
    {
        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid {
            get
            {
                return (Count == 0);
            }
        }

        /// <summary>
        /// Liefert die aktuellen Fehlermeldungen zurück, wobei jeder Fehler in einer neuen Zeile steht
        /// </summary>
        /// <returns>Fehlermeldungen</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ValidationEntry item in this)
            {
                sb.Append(item.Message).Append("\r\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Liefert die aktuellen Fehlermeldungen zurück, wobei jeder Fehler in einer neuen Zeile mit HTML-Tag Brakeline steht
        /// </summary>
        /// <returns>Fehlermeldungen</returns>
        public string ToHhtmlString()
        {
            return ToString().Replace("\r\n", "<br/>");
        }

        /// <summary>
        /// Liefert die aktuellen Fehlermeldungen zurück für die Alert-Anweisung aus C# herraus, wobei jeder Fehler in einer neuen Zeile steht
        /// </summary>
        /// <returns>Fehlermeldungen</returns>
        public string ToJavaScriptAlertString()
        {
            return ToString().Replace("\r\n", "\\n");
        }
    }
}
