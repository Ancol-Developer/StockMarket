using System.ComponentModel.DataAnnotations;

namespace StockMarket.Entities.CustomValidations;

public class OrderAndSellDateTimeValidation : ValidationAttribute
{
    private readonly DateTime _minimumDate;
    public string DefaultErrorMessage { get; set; } = "Order date should be greater than or equal to {0}";

    public OrderAndSellDateTimeValidation(string minimumDateString)
    {
        _minimumDate = Convert.ToDateTime(minimumDateString);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not null)
        {
            DateTime date = Convert.ToDateTime(value.ToString());
            if(date > _minimumDate)
            {
                return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, _minimumDate.ToString("yyyy-MM-dd")), new string[] { nameof(validationContext.MemberName) });
            }

            return ValidationResult.Success;
        }
        return null;
    }
}
