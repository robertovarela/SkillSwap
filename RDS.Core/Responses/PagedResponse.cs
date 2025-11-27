namespace RDS.Core.Responses;

/// <summary>
/// Resposta de API com paginação.
/// </summary>
public class PagedResponse<T> : Response<IEnumerable<T>>
{
    /// <summary>
    /// Número da página atual.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Tamanho da página (quantidade de registros por página).
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total de registros encontrados.
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    /// Total de páginas disponíveis.
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);

    public static PagedResponse<T> SuccessPagedResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecords, string message = "Operação realizada com sucesso")
    {
        return new PagedResponse<T>
        {
            Success = true,
            Data = data,
            Message = message,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords
        };
    }
}