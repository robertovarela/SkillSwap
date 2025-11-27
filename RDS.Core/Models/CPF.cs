using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace RDS.Core.Models;

/// <summary>
/// Representa um número de CPF (Cadastro de Pessoas Físicas) como um Value Object.
/// Garante que um CPF só pode ser criado se for válido.
/// </summary>
[TypeConverter(typeof(CpfTypeConverter))]
public readonly record struct Cpf
{
    public string Number { get; }

    // Construtor usado pelos métodos de parsing
    private Cpf(string number)
    {
        Number = new string(number.Where(char.IsDigit).ToArray());
    }

    // Construtor público que valida
    public Cpf(string number, bool validate = true)
    {
        if (validate && !IsValid(number))
        {
            throw new ArgumentException("Número de CPF inválido.", nameof(number));
        }
        Number = new string(number.Where(char.IsDigit).ToArray());
    }

    public override string ToString() => Number;

    /// <summary>
    /// Formata o CPF com a máscara padrão (000.000.000-00).
    /// </summary>
    public string ToFormattedString()
    {
        if (string.IsNullOrEmpty(Number) || Number.Length != 11)
            return Number ?? string.Empty;

        return Convert.ToUInt64(Number).ToString(@"000\.000\.000\-00");
    }

    /// <summary>
    /// Tenta criar uma instância de CPF a partir de uma string.
    /// </summary>
    public static bool TryParse(string? value, out Cpf cpf)
    {
        if (IsValid(value))
        {
            cpf = new Cpf(value!, false); // Não precisa validar de novo
            return true;
        }

        cpf = default;
        return false;
    }

    /// <summary>
    /// Cria uma instância de CPF a partir de uma string. Lança uma exceção se o formato for inválido.
    /// </summary>
    public static Cpf Parse(string value)
    {
        if (!TryParse(value, out var cpf))
        {
            throw new ArgumentException("Número de CPF inválido.", nameof(value));
        }
        return cpf;
    }

    /// <summary>
    /// Valida um número de CPF.
    /// </summary>
    public static bool IsValid(string? cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        var cleanCpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cleanCpf.Length != 11)
            return false;

        if (cleanCpf.All(c => c == cleanCpf[0]))
            return false;

        var sum = 0;
        for (var i = 0; i < 9; i++)
            sum += (cleanCpf[i] - '0') * (10 - i);

        var remainder = sum % 11;
        var digit1 = remainder < 2 ? 0 : 11 - remainder;

        if ((cleanCpf[9] - '0') != digit1)
            return false;

        sum = 0;
        for (var i = 0; i < 10; i++)
            sum += (cleanCpf[i] - '0') * (11 - i);

        remainder = sum % 11;
        var digit2 = remainder < 2 ? 0 : 11 - remainder;

        return (cleanCpf[10] - '0') == digit2;
    }

    public static implicit operator string(Cpf cpf) => cpf.Number;
    public static implicit operator Cpf(string value) => Parse(value);
}

/// <summary>
/// Permite que o Cpf seja usado em cenários de model binding e conversão de tipo.
/// </summary>
public class CpfTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
    {
        if (value is string s)
        {
            return Cpf.Parse(s);
        }
        return base.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is Cpf cpf)
        {
            return cpf.Number;
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}
