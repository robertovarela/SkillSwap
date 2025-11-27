using System.Globalization;
using Microsoft.Extensions.Configuration;
using RDS.Core.Libraries.Services;

namespace RDS.Infrastructure.Services;

public class CurrencyParser : ICurrencyParser
{
    private readonly CultureInfo _culture;
    public CurrencyParser(IConfiguration config)
    {
        var cultureName = config["DefaultCulture"] ?? "pt-BR";
        _culture = new CultureInfo(cultureName);
    }

    public decimal Parse(string maskedValue)
    {
        if (string.IsNullOrWhiteSpace(maskedValue)) return 0m;
        if (decimal.TryParse(maskedValue, NumberStyles.Number, _culture, out var v))
            return v;
        throw new FormatException($"Valor '{maskedValue}' inválido para cultura '{_culture.Name}'.");
    }
}