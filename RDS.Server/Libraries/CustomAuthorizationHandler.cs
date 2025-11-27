using System.Net.Http.Headers;

namespace RDS.Server.Libraries;

public class CustomAuthorizationHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Solo agregamos el token si estamos en modo servidor
        if (httpContextAccessor.HttpContext != null)
        {
            // En un entorno real, obtendrías el token de autenticación actual
            // por ejemplo de las cookies de autenticación o del usuario
            var token = httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated == true
                ? "servidor-token" // Aquí obtendrías el token real
                : null;

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        return base.SendAsync(request, cancellationToken);
    }
}
