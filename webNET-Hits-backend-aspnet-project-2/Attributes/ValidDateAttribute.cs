using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace webNET_Hits_backend_aspnet_project_2.Attributes
{
    public class ValidDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date = ConvertStringToTime(value.ToString());
            DateTime minDate = ConvertStringToTime("01.01.1900 00:00:00".ToString());
            DateTime maxDate = DateTime.UtcNow;

            if (date < minDate)
            {
                return new ValidationResult($"BithDate must be greater than {minDate}");
            }
            if (date > maxDate)
            {                                           
                return new ValidationResult($"BithDate must be lower than {maxDate}");
            }

            return ValidationResult.Success;
        }

        private DateTime ConvertStringToTime(string value)
        {
            return DateTime.ParseExact(value.ToString(), "dd.MM.yyyy HH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
