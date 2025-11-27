namespace RDS.Core.Enums;

/// <summary>
/// Define as opções de ordenação disponíveis para serviços.
/// </summary>
public enum ServiceOrderType
{
    /// <summary>
    /// Mais recentes primeiro (padrão)
    /// </summary>
    NewestFirst,

    /// <summary>
    /// Mais antigos primeiro
    /// </summary>
    OldestFirst,

    /// <summary>
    /// Título A-Z
    /// </summary>
    TitleAsc,

    /// <summary>
    /// Título Z-A
    /// </summary>
    TitleDesc
}