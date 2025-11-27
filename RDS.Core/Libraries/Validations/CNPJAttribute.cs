using System.ComponentModel.DataAnnotations;

namespace RDS.Core.Libraries.Validations;

public class CnpjAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null) return true;

        string cnpj = (string)value;
        return IsCnpj(cnpj);
    }

    private static bool IsCnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return false;

        // Remove caracteres especiais
        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        if (cnpj.Length != 14)
            return false;

        // Verifica se todos os dígitos são iguais
        if (cnpj.Distinct().Count() == 1)
            return false;

        var multiplicador1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicador2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCnpj = cnpj[..12];
        var soma = multiplicador1.Select((m, i) => (cnpj[i] - '0') * m).Sum();
        var resto = (soma % 11) < 2 ? 0 : 11 - (soma % 11);

        var digito = resto.ToString();
        tempCnpj += digito;

        soma = multiplicador2.Select((m, i) => (tempCnpj[i] - '0') * m).Sum();
        resto = (soma % 11) < 2 ? 0 : 11 - (soma % 11);

        digito += resto.ToString();

        return cnpj.EndsWith(digito);
    }
}

