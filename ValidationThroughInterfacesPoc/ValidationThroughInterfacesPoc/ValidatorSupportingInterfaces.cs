using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ValidationThroughInterfacesPoc
{

    /// <summary>
    /// Simple validator that applies validation attributes logic of an instance, but also consider validation attributes of derived validation attributes from interfaces the type is implementing
    /// </summary>
    public static class ValidatorSupportingInterfaces
    {
        public static ValidationResult Validate(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var validationErrors = TypeDescriptor.GetProperties(obj.GetType()).Cast<PropertyDescriptor>().SelectMany(
                    pd => pd.Attributes.OfType<ValidationAttribute>()
                        .Where(va => !va.IsValid(pd.GetValue(obj))).Select(validationAttr =>
                            new { ValidationErrorMessage = validationAttr.ErrorMessage, MemberName = pd.Name }))
                .Union((obj.GetType().GetInterfaces().SelectMany(ifc => ifc.GetProperties().SelectMany(ifcp => ifcp
                    .GetCustomAttributes().OfType<ValidationAttribute>()
                    .Where(va => !va.IsValid(ifcp.GetValue(obj))).Select(
                        validationAttr => new
                        { ValidationErrorMessage = validationAttr.ErrorMessage, MemberName = ifcp.Name }
                    ))))).ToList();

            return !validationErrors.Any()
                ? ValidationResult.Success
                : new ValidationResult(string.Join(",", validationErrors.Select(ve => ve.ValidationErrorMessage)),
                    validationErrors.Select(ve => ve.MemberName));
        }
    }
}
