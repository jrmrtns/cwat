using System;
using System.Windows.Markup;
using Cellent.Template.Client.Core.Core.Resources;

namespace Cellent.Template.Client.Core.MarkupExtensions
{
    /// <summary>
    /// Erweiterung für die lokalisierung der Oberfläche
    /// </summary>
    public class TranslateExtension : MarkupExtension
    {        
        /// <summary>
        /// Initialisiert die Extension
        /// </summary>
        /// <param name="key">der Bezeichner, der übersetzt werden soll</param>
        public TranslateExtension(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Der Key, der übersetzt werden soll
        /// </summary>
        [ConstructorArgument("key")]
        public string Key { get; set; }

        /// <summary>
        /// Stellt die Übersetztung bereit, falls vorhanden
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>die Übersetzung für Key. Falls keine Übersetzung vorhanden ist, Key</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Translation.Translate(Key);
        }
    }
}
