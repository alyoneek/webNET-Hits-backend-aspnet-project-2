using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_2.Attributes
{
    public class ValidDeliveryTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date = ConvertStringToTime(value.ToString());
            DateTime minDate = DateTime.UtcNow.AddHours(1);
            DateTime maxDate = DateTime.UtcNow.AddMonths(1);

            if (date < minDate)
            {
                return new ValidationResult($"Order time must be greater than {minDate}");
            }
            if (date > maxDate)
            {
                return new ValidationResult($"Order time must be lower than {maxDate}");
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
