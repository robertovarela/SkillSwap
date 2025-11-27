using System.Globalization;

namespace RDS.Core.Utils;

/// <summary>
/// Fornece instâncias de CultureInfo personalizadas para a aplicação.
/// </summary>
public class CultureHelper
{
    /// <summary>
    /// Fornece uma cultura baseada em 'pt-BR' com o símbolo da moeda alterado para 'SD$'.
    /// </summary>
    public CultureInfo SkillDollarCulture { get; }

    public CultureHelper()
    {
        var ptBrCulture = new CultureInfo("pt-BR");
        SkillDollarCulture = (CultureInfo)ptBrCulture.Clone();
        SkillDollarCulture.NumberFormat.CurrencySymbol = "SD$";
    }
}
