using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Cellent.Template.Client.Core.Extensions
{
    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Adds value on end of the current string
        /// </summary>
        /// <param name="instance">current string</param>
        /// <param name="value">value to add</param>
        /// <param name="separator">value that separates joined values</param>
        public static string AddValueOnEnd(this string instance, string value, string separator)
        {
            if (string.IsNullOrWhiteSpace(instance))
                instance += value;
            else
                instance += string.Format("{0} {1}", separator.Trim(), value);

            return instance;
        }

        /// <summary>
        /// Serialize a object to string
        /// </summary>
        /// <param name="objectInstance">object to serialize</param>
        /// <returns>Results as XML content</returns>
        public static string XmlSerializeToString(this object objectInstance)
        {
            var serializer = new XmlSerializer(objectInstance.GetType());
            var sb = new StringBuilder();

            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, objectInstance);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Deserialize a string to a object
        /// </summary>
        /// <typeparam name="T">Object Typ</typeparam>
        /// <param name="objectData">XML content to parse as object</param>
        /// <returns>Object</returns>
        public static T XmlDeserializeFromString<T>(this string objectData)
        {
            return (T)XmlDeserializeFromString(objectData, typeof(T));
        }

        /// <summary>
        /// Deserialize a string to a object by given object type
        /// </summary>
        /// <param name="objectData">XML content to parse as object</param>
        /// <param name="type">Object typ</param>
        /// <returns>Object</returns>
        public static object XmlDeserializeFromString(this string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
