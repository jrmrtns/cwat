namespace Cellent.Template.Common.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidationEntry
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationEntry"/> class.
        /// </summary>
        /// <param name="isValid">if set to <c>true</c> [is valid].</param>
        /// <param name="message">The message.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="value">The value.</param>
        public ValidationEntry(bool isValid, string message, string fieldName, object value)
        {
            _isValid = isValid;
            _message = message;
            _fieldName = fieldName;
            _value = value;
        }

        #endregion

        #region Fields
        private readonly bool _isValid;
        private readonly string _message;
        private readonly string _fieldName;
        private readonly object _value;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        /// <value>
        /// The name of the field.
        /// </value>
        public string FieldName
        {
            get { return _fieldName; }
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get { return _message; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get { return _isValid; }
        }
        #endregion
    }
}
