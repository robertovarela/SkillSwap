using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace RDS.Server.Libraries;
public class IdentityAccessTokenAdapter(
    AuthenticationStateProvider authStateProvider,
    IHttpContextAccessor httpContextAccessor)
    : IAccessTokenProvider
{
    private readonly AuthenticationStateProvider _authStateProvider = authStateProvider;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    [Obsolete("Obsolete")]
    public ValueTask<AccessTokenResult> RequestAccessToken()
    {
        // En modo servidor, utilizamos un token simplificado
        var token = new AccessToken {
            Value = "",
            Expires = DateTimeOffset.Now.AddHours(1)
        };

        return new ValueTask<AccessTokenResult>(
            new AccessTokenResult(AccessTokenResultStatus.Success, token, null!));
    }

    [Obsolete("Obsolete")]
    public ValueTask<AccessTokenResult> RequestAccessToken(AccessTokenRequestOptions options)
    {
        return RequestAccessToken();
    }

}
