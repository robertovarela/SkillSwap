namespace RDS.Core.Responses;

/// <summary>
/// Modelo genérico para respostas de API.
/// </summary>
/// <typeparam name="T">Tipo dos dados retornados.</typeparam>
public class Response<T>
{
    /// <summary>
    /// Indica se a operação foi bem-sucedida.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Mensagem adicional sobre o resultado da operação.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Dados retornados pela operação (se aplicável).
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Construtor padrão.
    /// </summary>
    public Response() { }

    /// <summary>
    /// Cria uma resposta de sucesso.
    /// </summary>
    public static Response<T> SuccessResponse(T data, string message = "Operação realizada com sucesso")
    {
        return new Response<T> { Success = true, Data = data, Message = message };
    }

    /// <summary>
    /// Cria uma resposta de erro.
    /// </summary>
    public static Response<T> ErrorResponse(string message)
    {
        return new Response<T> { Success = false, Message = message };
    }

    public static Response<T> ErrorResponse(string message, T data)
        => new() { Success = false, Message = message, Data = data };
}


//Exemplo de uso

/*
/// <summary>
/// Retorna a lista de todas as categorias.
/// </summary>
/// <returns>Lista de categorias.</returns>
/// <response code="200">Lista retornada com sucesso.</response>
[HttpGet]
[ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoriaDto>>), StatusCodes.Status200OK)]
public IActionResult GetAll()
{
    var categorias = new List<CategoriaDto>
    {
        new CategoriaDto { Id = 1, Nome = "Tecnologia" },
        new CategoriaDto { Id = 2, Nome = "Design" }
    };

    return Ok(ApiResponse<IEnumerable<CategoriaDto>>.SuccessResponse(categorias));
}


/// <summary>
/// Busca uma categoria pelo ID.
/// </summary>
[HttpGet("{id}")]
[ProducesResponseType(typeof(ApiResponse<CategoriaDto>), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
public IActionResult GetById(long id)
{
    var categoria = new CategoriaDto { Id = id, Nome = "Tecnologia" };

    if (categoria == null)
        return NotFound(ApiResponse<object>.ErrorResponse("Categoria não encontrada"));

    return Ok(ApiResponse<CategoriaDto>.SuccessResponse(categoria));
}
*/
