using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace RDS.Server.Libraries;

public class AuthenticationHeaderHandler(IAccessTokenProvider tokenProvider) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Intentar obtener el token de acceso
        var tokenResult = await tokenProvider.RequestAccessToken();

        if (tokenResult.TryGetToken(out var token))
        {
            // Agregar el token como encabezado de autorización
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token.Value);
        }

        // Continuar con la solicitud
        return await base.SendAsync(request, cancellationToken);
    }
}
