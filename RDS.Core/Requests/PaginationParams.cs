namespace RDS.Core.Requests;

/// <summary>
/// Representa os parâmetros de paginação para consultas.
/// </summary>
public class PaginationParams
{
    /// <summary>
    /// Número da página (padrão 1).
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Quantidade de itens por página (padrão 10).
    /// </summary>
    public int PageSize { get; set; } = 10;
}