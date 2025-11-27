namespace RDS.Core.Requests;

/// <summary>
/// Parâmetros de filtro para buscas de perfis profissionais.
/// </summary>
public class ProfessionalProfileFilterParams : FilterParams
{
    /// <summary>
    /// Indica se o perfil é premium ou não.
    /// Valores possíveis: true, false ou null (não filtrado).
    /// </summary>
    public bool? IsPremium { get; set; }
}