using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RDS.Core.Libraries.Services;
using RDS.Core.Models;
using RDS.Server.Components.Pages;

namespace RDS.Server.Libraries;

public class UserContextService(
    AuthenticationStateProvider authProv,
    UserManager<ApplicationUser> userMgr,
    NavigationManager nav,
    IProfessionalProfileService professionalProfileService)
    : IUserContextService
{
    // Propriedades públicas SOMENTE leitura na interface
    public long UserId { get; private set; }
    public long ProfessionalId { get; private set; }
    public bool IsAuthenticated { get; private set; }
    public bool HasProfessionalProfile { get; private set; }
    private Task<UserContextResult>? _initializationTask; // Gatekeeper Task

    public async Task<UserAuthResult> GetUserAuthAsync()
    {
        var authState = await authProv.GetAuthenticationStateAsync();
        var userPrincipal = authState.User;
        if (!(userPrincipal.Identity?.IsAuthenticated ?? false))
        {
            nav.NavigateTo($"{AppRoutes.Login}", forceLoad: true);
            return new UserAuthResult(0, true);
        }

        var currentUser = await userMgr.GetUserAsync(userPrincipal);
        if (currentUser == null || currentUser.Id == 0)
        {
            return new UserAuthResult(0, true);
        }

        return new UserAuthResult(currentUser.Id, false);
    }

    public async Task<ProfessionalResult> GetProfessionalProfileId(long userId)
    {
        var professional = await professionalProfileService.GetByUserIdAsync(userId);
        return professional != null ? new ProfessionalResult(professional.Id, false) : new ProfessionalResult(0, true);
    }

    public async Task<UserContextResult> InitializeUserContextAsync()
    {
        // Se a tarefa de inicialização já existe, retorna ela.
        // Isso garante que chamadas concorrentes aguardem a mesma operação.
        _initializationTask ??= InitializeInternalAsync();
        return await _initializationTask;
    }

    private async Task<UserContextResult> InitializeInternalAsync()
    {
        var authResult = await GetUserAuthAsync();
        UserId = authResult.UserId;
        IsAuthenticated = !authResult.ShowAlert;

        if (!IsAuthenticated)
        {
            ProfessionalId = 0;
            HasProfessionalProfile = false;
            return new UserContextResult(UserId, 0, true, false, false);
        }

        var professionalResult = await GetProfessionalProfileId(UserId);
        ProfessionalId = professionalResult.ProfessionalId;
        HasProfessionalProfile = !professionalResult.ShowAlert;

        if (!HasProfessionalProfile)
        {
            return new UserContextResult(UserId, ProfessionalId, false, true, false);
        }

        return new UserContextResult(UserId, ProfessionalId, false, false, true);
    }

    public async Task<string> GetUserEmail(string userId)
    {
        var user = await userMgr.FindByIdAsync(userId);
        return user?.Email ?? "User not found";
    }

    public void NavigateToLogin()
        => nav.NavigateTo($"{AppRoutes.Login}", forceLoad: true);

    public void NavigateToInstructions()
        => nav.NavigateTo($"{AppRoutes.UserNotFound}");
}

// Records sem mudanças
public record UserAuthResult(long UserId, bool ShowAlert);

public record ProfessionalResult(long ProfessionalId, bool ShowAlert);

public record UserContextResult(
    long UserId,
    long ProfessionalId,
    bool ShowCurrentUserNotFoundAlert,
    bool ShowProfessionalNotFoundAlert,
    bool ShowContent
);

public interface IUserContextService
{
    long UserId { get; }
    long ProfessionalId { get; }
    bool IsAuthenticated { get; }
    bool HasProfessionalProfile { get; }

    Task<UserAuthResult> GetUserAuthAsync();
    Task<ProfessionalResult> GetProfessionalProfileId(long userId);
    Task<UserContextResult> InitializeUserContextAsync();
    Task<string> GetUserEmail(string userId);
    void NavigateToLogin();
    void NavigateToInstructions();
}