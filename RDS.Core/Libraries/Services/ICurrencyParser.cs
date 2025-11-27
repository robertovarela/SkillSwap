namespace RDS.Core.Libraries.Services;

public interface ICurrencyParser
{
    decimal Parse(string maskedValue);
}
