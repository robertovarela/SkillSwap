using Microsoft.AspNetCore.Components.Authorization;

namespace RDS.Server.Libraries;

public static class CheckAuthorization
{
    /// <summary>
    /// Verifica se o usuário está autenticado e possui a role especificada
    /// </summary>
    /// <param name="authenticationStateProvider">Provider de estado de autenticação</param>
    /// <param name="role">Nome da role a ser verificada</param>
    /// <returns>True se o usuário estiver autenticado e possuir a role</returns>
    public static async Task<bool> CheckUserAuthorizationAsync(
        AuthenticationStateProvider authenticationStateProvider,
        string role)
    {
        try
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            return authState.User.Identity?.IsAuthenticated == true &&
                   authState.User.IsInRole(role);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Verifica se o usuário está autenticado e possui uma das roles especificadas
    /// </summary>
    /// <param name="authenticationStateProvider">Provider de estado de autenticação</param>
    /// <param name="roles">Array de roles a serem verificadas</param>
    /// <returns>True se o usuário estiver autenticado e possuir pelo menos uma das roles</returns>
    public static async Task<bool> CheckUserAuthorizationAsync(
        AuthenticationStateProvider authenticationStateProvider,
        params string[] roles)
    {
        try
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            if (authState.User.Identity?.IsAuthenticated != true)
                return false;

            return roles.Any(role => authState.User.IsInRole(role));
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Verifica se o usuário está apenas autenticado (sem verificar roles)
    /// </summary>
    /// <param name="authenticationStateProvider">Provider de estado de autenticação</param>
    /// <returns>True se o usuário estiver autenticado</returns>
    public static async Task<bool> IsUserAuthenticatedAsync(
        AuthenticationStateProvider authenticationStateProvider)
    {
        try
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            return authState.User.Identity?.IsAuthenticated == true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Obtém o nome do usuário autenticado
    /// </summary>
    /// <param name="authenticationStateProvider">Provider de estado de autenticação</param>
    /// <returns>Nome do usuário ou null se não autenticado</returns>
    public static async Task<string?> GetUserNameAsync(
        AuthenticationStateProvider authenticationStateProvider)
    {
        try
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            return authState.User.Identity?.IsAuthenticated == true
                ? authState.User.Identity.Name
                : null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Obtém todas as roles do usuário autenticado
    /// </summary>
    /// <param name="authenticationStateProvider">Provider de estado de autenticação</param>
    /// <returns>Lista de roles ou lista vazia se não autenticado</returns>
    public static async Task<IEnumerable<string>> GetUserRolesAsync(
        AuthenticationStateProvider authenticationStateProvider)
    {
        try
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            if (authState.User.Identity?.IsAuthenticated != true)
                return Enumerable.Empty<string>();

            return authState.User.Claims
                .Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                .Select(c => c.Value);
        }
        catch
        {
            return Enumerable.Empty<string>();
        }
    }
}