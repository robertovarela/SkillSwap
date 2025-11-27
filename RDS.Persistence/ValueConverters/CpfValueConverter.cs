using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RDS.Core.Models;

namespace RDS.Persistence.ValueConverters;

/// <summary>
/// Converte o Value Object Cpf para uma string no banco de dados e vice-versa.
/// </summary>
public class CpfValueConverter : ValueConverter<Cpf, string>
{
    public CpfValueConverter() : base(
        // Converte de Cpf para string (para salvar no banco)
        v => v.Number,
        // Converte de string para Cpf (para ler do banco)
        v => Cpf.Parse(v))
    {
    }
}
