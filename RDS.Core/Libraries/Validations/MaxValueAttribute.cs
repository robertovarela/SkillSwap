using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace RDS.Core.Libraries.Validations;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class MaxValueAttribute(double max) : ValidationAttribute
{
    private decimal Max { get; } = Convert.ToDecimal(max);

    public override bool IsValid(object? value)
    {
        if (value is null) return true;
        if (decimal.TryParse(value.ToString(), out var d))
            return d <= Max;
        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        // name -> {0}, Max -> {1}
        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString!, name, Max);
    }
}
