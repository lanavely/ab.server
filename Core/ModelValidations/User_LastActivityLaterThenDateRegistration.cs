using System.ComponentModel.DataAnnotations;

namespace Core.ModelValidations
{
    public class User_LastActivityLaterThenDateRegistration : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            return validationContext.ObjectInstance is User user && user.DateLastActivity < user.DateRegistration
                ? new("DateLastActivity cannot be earlier than the DateRegistration")
                : ValidationResult.Success;
        }
    }
}