using Microsoft.EntityFrameworkCore;

namespace RDS.Core.Helpers;

/// <summary>
/// Extensões para ordenação dinâmica de IQueryable.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Aplica ordenação dinâmica em uma consulta.
    /// </summary>
    public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, string orderBy, string? orderDir)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
            return query;

        // Normaliza orderDir e protege contra null
        var dir = (orderDir ?? string.Empty).ToLowerInvariant();

        if (dir == "desc")
            return query.OrderByDescending(e => EF.Property<object?>(e!, orderBy));
        else
            return query.OrderBy(e => EF.Property<object?>(e!, orderBy));

        //Ajustar com IA
    }
}