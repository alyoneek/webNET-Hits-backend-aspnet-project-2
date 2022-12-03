using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_2.Helpers
{
    public class ValidEnumAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(ErrorMessage ?? "This field is required");
            }

            return ValidationResult.Success;
        }
    }
}
