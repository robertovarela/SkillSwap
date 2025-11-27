using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace RDS.Core.Libraries.Validations;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class MinValueAttribute(double min) : ValidationAttribute
{
    private decimal Min { get; } = Convert.ToDecimal(min);

    public override bool IsValid(object? value)
    {
        if (value is null) return true;
        if (decimal.TryParse(value.ToString(), out var d))
            return d >= Min;
        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        // name -> {0}, Min -> {1}
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString!, name, Min);
    }
}