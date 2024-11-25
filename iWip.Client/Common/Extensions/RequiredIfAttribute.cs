/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using System.ComponentModel.DataAnnotations;

namespace iWip.Client.Common.Extensions
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredIfAttribute : RequiredAttribute
    {
        RequiredAttribute _innerAttribute = new RequiredAttribute();
        public bool IsInverted { get; set; } = false;
        public string DependentProperty { get; private set; }
        public object TargetValue { get; private set; }

        public RequiredIfAttribute(string dependentProperty, object targetValue, string errorMessage = "", string errorMessageResourceName = "", Type errorMessageResourceType = null)
          : base()
        {
            DependentProperty = dependentProperty;
            TargetValue = targetValue;
            ErrorMessage = errorMessage;
            ErrorMessageResourceName = errorMessageResourceName;
            ErrorMessageResourceType = errorMessageResourceType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var field = validationContext.ObjectType.GetProperty(DependentProperty);
            if (field != null)
            {
                var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                //if ((dependentValue == null && TargetValue == null) || (dependentValue.Equals(TargetValue)))
                //{
                if (!_innerAttribute.IsValid(value))
                {
                    string name = validationContext.DisplayName;
                    string specificErrorMessage = ErrorMessage;
                    if (specificErrorMessage.Length < 1 && string.IsNullOrEmpty(ErrorMessageResourceName))
                        specificErrorMessage = $"{name} is required.";

                    return new ValidationResult(specificErrorMessage, new[] { validationContext.MemberName });
                }
                //}
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(FormatErrorMessage(DependentProperty));
            }
        }
    }
}
