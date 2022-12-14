using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace webNET_Hits_backend_aspnet_project_2.Attributes
{
    public class ValidPasswordAttribute : ValidationAttribute
    {
        private int MinLength { get; }
        private int MaxLength { get; }
        public ValidPasswordAttribute(int minLength, int maxLength)
        {
            MinLength= minLength;
            MaxLength= maxLength;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string password = value.ToString();

            if (password.Length < MinLength)
            {
                return new ValidationResult($"Min length of password is {MinLength}");
            }
            if (password.Length > MaxLength)
            {
                return new ValidationResult($"Max length of password is {MaxLength}");
            }
            if (Regex.IsMatch(password, @"^(?=.*\s).+$"))
            {
                return new ValidationResult("Password can't contain whitespaces");
            }
            if (!Regex.IsMatch(password, @"^(?=.*\d).+$"))
            {
                return new ValidationResult("Password must contain at least one digit");
            }

            return ValidationResult.Success;
        }
    }
}
