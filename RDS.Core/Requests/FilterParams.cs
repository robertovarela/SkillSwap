namespace RDS.Core.Requests;

/// <summary>
/// Representa parâmetros para filtro, ordenação e paginação de consultas.
/// </summary>
public class FilterParams : PaginationParams
{
    /// <summary>
    /// Texto de busca livre para filtrar registros por campos configurados.
    /// Exemplo: O usuário irá buscar por registros que contenham "software" nos campos de busca.
    /// </summary>
    public string? Search { get; set; }

    /// <summary>
    /// Nome do campo para ordenar os resultados.
    /// Campos permitidos variam por endpoint - consulte a documentação específica.
    /// </summary>
    /// <example>Name</example>
    public string? OrderBy { get; set; }

    /// <summary>
    /// Direção da ordenação dos resultados.
    /// Valores permitidos: "asc" (crescente - valor padrão) ou "desc" (decrescente)
    /// </summary>
    /// <example>asc</example>
    public string? OrderDir { get; set; } = "asc"; // Padrão ascendente

    /// <summary>
    /// Filtrar os serviços disponíveis por categoria.
    /// </summary>
    public long? CategoryId { get; set; }
}