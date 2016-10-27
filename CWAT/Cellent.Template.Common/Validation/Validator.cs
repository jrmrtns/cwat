using System;
using System.Reflection;
using Cellent.Template.Common.Interfaces.Validation;
using Cellent.Template.Common.Validation.Validators;

namespace Cellent.Template.Common.Validation
{
    /// <summary>
    /// Validierung
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Validates the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static ValidationSummary Validate(object o)
        {
            return Validate(o, "Default");
        }

        /// <summary>
        /// Validates the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="summary">The summary.</param>
        /// <returns></returns>
        public static ValidationSummary Validate(object o, ValidationSummary summary)
        {
            return Validate(o, "Default", summary);
        }

        /// <summary>
        /// Validates the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="groupname">The groupname.</param>
        /// <param name="summary">The summary.</param>
        /// <returns></returns>
        public static ValidationSummary Validate(object o, string groupname, ValidationSummary summary)
        {
            Type type = o.GetType();
            Type[] interfaces = type.GetInterfaces();
            foreach (Type inteface in interfaces)
            {
                DoValidate(o, inteface, groupname, ref summary);
            }

            while (type != null)
            {
                DoValidate(o, type, groupname, ref summary);
                type = type.BaseType;
            }
            return summary;
        }

        /// <summary>
        /// Validates the specified o.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="groupname">The groupname.</param>
        /// <returns></returns>
        public static ValidationSummary Validate(object o, string groupname)
        {
            ValidationSummary summary = new ValidationSummary();
            Type type = o.GetType();
            Type[] interfaces = type.GetInterfaces();
            foreach (Type inteface in interfaces)
            {
                DoValidate(o, inteface, groupname, ref summary);
            }

            while (type != null)
            {
                DoValidate(o, type, groupname, ref summary);
                type = type.BaseType;
            }
            return summary;
        }

        private static void DoValidate(object o, Type type, string groupname, ref ValidationSummary summary)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                object[] attributes = field.GetCustomAttributes(true);
                foreach (Attribute attribute in attributes)
                {
                    IValidator validator = attribute as IValidator;
                    if (validator != null)
                    {
                        if(validator.IsInGroup(groupname))
                        {
                            object value;
                            string memberName;
                            try
                            {
                                value = field.GetValue(o);
                                memberName = field.Name;
                            }
                            catch
                            {
                                break;
                            }
                            validator.IsValid(value, String.IsNullOrEmpty(validator.Alias) ? memberName : validator.Alias, ref summary);
                        }
                    }
                }
            }

            ICustomValidatable validatable = o as ICustomValidatable;
            PropertyInfo[] props = type.GetProperties(BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo prop in props)
            {
                object[] attributes = prop.GetCustomAttributes(true);
                foreach (Attribute attribute in attributes)
                {
                    IValidator validator = attribute as IValidator;
                    if (validator != null)
                    {
                        if (validator.IsInGroup(groupname))
                        {
                            object value;
                            string memberName;
                            try
                            {
                                value = prop.GetValue(o, null);
                                memberName = prop.Name;
                            }
                            catch
                            {
                                break;
                            }

                            validator.IsValid(value, string.IsNullOrEmpty(validator.Alias) ? memberName : validator.Alias, ref summary);
                        }
                    }
                }
                if (validatable != null && type == o.GetType())
                {
                    string additionalValidation = validatable.ExecuteCustomValidation(prop.Name);
                    if (!string.IsNullOrEmpty(additionalValidation))
                        summary.Add(new ValidationEntry(false, additionalValidation, prop.Name, null));
                }
            }
        }
    }
}
