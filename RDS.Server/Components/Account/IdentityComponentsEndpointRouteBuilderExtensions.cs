using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RDS.Core.Models;
using RDS.Server.Components.Account.Pages.Manage;

namespace RDS.Server.Components.Account
{
    internal static class IdentityComponentsEndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
        {
            ArgumentNullException.ThrowIfNull(endpoints);

            var accountGroup = endpoints.MapGroup("/Account");

            accountGroup.MapPost("/PerformExternalLogin", (
                HttpContext context,
                [FromServices] SignInManager<ApplicationUser> signInManager,
                [FromForm] string provider,
                [FromForm] string returnUrl) =>
            {
                // Change the redirect URL to our new, non-Blazor API endpoint
                var redirectUrl = UriHelper.BuildRelative(
                    context.Request.PathBase,
                    "/Account/ExternalLoginCallback", // This is our new API endpoint
                    QueryString.Create("ReturnUrl", returnUrl));

                var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
                properties.Items["prompt"] = "select_account";
                return TypedResults.Challenge(properties, [provider]);
            });

            // New API endpoint to handle the callback from the external provider
            accountGroup.MapGet("/ExternalLoginCallback", async (
                HttpContext context,
                [FromServices] SignInManager<ApplicationUser> signInManager,
                [FromServices] UserManager<ApplicationUser> userManager,
                [FromServices] ILoggerFactory loggerFactory) =>
            {
                var logger = loggerFactory.CreateLogger("ExternalLoginCallback");
                
                var returnUrl = context.Request.Query["ReturnUrl"].FirstOrDefault();
                if (string.IsNullOrEmpty(returnUrl))
                {
                    returnUrl = "/";
                }

                var loginInfo = await signInManager.GetExternalLoginInfoAsync();
                if (loginInfo is null)
                {
                    logger.LogWarning("Error loading external login information.");
                    return Results.Redirect($"/Account/Login?message={Uri.EscapeDataString("Error loading external login information.")}");
                }

                // Try to sign in the user with this external login provider
                var signInResult = await signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, isPersistent: false, bypassTwoFactor: true);
                if (signInResult.Succeeded)
                {
                    logger.LogInformation("User {UserId} logged in with {LoginProvider} provider.", loginInfo.Principal.FindFirstValue(ClaimTypes.NameIdentifier), loginInfo.LoginProvider);
                    return Results.LocalRedirect(returnUrl);
                }
                if (signInResult.IsLockedOut)
                {
                    return Results.Redirect("/Account/Lockout");
                }

                // If sign-in failed, the user may not have an account yet, or just doesn't have a login for this provider.
                // See if the user already has an account with the same email.
                var email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email);
                if (email is not null)
                {
                    var user = await userManager.FindByEmailAsync(email);
                    if (user is not null)
                    {
                        // Associate the new external login with the existing user
                        var addLoginResult = await userManager.AddLoginAsync(user, loginInfo);
                        if (addLoginResult.Succeeded)
                        {
                            // Sign in the user and redirect
                            await signInManager.SignInAsync(user, isPersistent: false, loginInfo.LoginProvider);
                            return Results.LocalRedirect(returnUrl);
                        }
                    }
                }

                // If we are here, the user needs to create an account.
                // Redirect to the Blazor page to complete registration.
                // The Blazor page will fetch the login info again using GetExternalLoginInfoAsync.
                return Results.Redirect($"/Account/ExternalLogin?ReturnUrl={Uri.EscapeDataString(returnUrl)}");
            });

            accountGroup.MapPost("/Logout", async (
                ClaimsPrincipal user,
                [FromServices] SignInManager<ApplicationUser> signInManager,
                [FromForm] string returnUrl) =>
            {
                await signInManager.SignOutAsync();
                return TypedResults.LocalRedirect($"~/{returnUrl}");
            });

            var manageGroup = accountGroup.MapGroup("/Manage").RequireAuthorization();

            manageGroup.MapPost("/LinkExternalLogin", async (
                HttpContext context,
                [FromServices] SignInManager<ApplicationUser> signInManager,
                [FromForm] string provider) =>
            {
                await context.SignOutAsync(IdentityConstants.ExternalScheme);
                var redirectUrl = UriHelper.BuildRelative(context.Request.PathBase, "/Account/Manage/ExternalLogins", QueryString.Create("Action", ExternalLogins.LinkLoginCallbackAction));
                var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, signInManager.UserManager.GetUserId(context.User));
                return TypedResults.Challenge(properties, [provider]);
            });

            var loggerFactory = endpoints.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var downloadLogger = loggerFactory.CreateLogger("DownloadPersonalData");

            manageGroup.MapPost("/DownloadPersonalData", async (
                HttpContext context,
                [FromServices] UserManager<ApplicationUser> userManager) =>
            {
                var user = await userManager.GetUserAsync(context.User);
                if (user is null)
                {
                    return Results.NotFound($"Unable to load user with ID '{userManager.GetUserId(context.User)}'.");
                }

                var userId = await userManager.GetUserIdAsync(user);
                downloadLogger.LogInformation("User with ID '{UserId}' asked for their personal data.", userId);

                var personalData = new Dictionary<string, string>();
                var personalDataProps = typeof(ApplicationUser).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
                foreach (var p in personalDataProps)
                {
                    personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
                }

                var logins = await userManager.GetLoginsAsync(user);
                foreach (var l in logins)
                {
                    personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
                }

                personalData.Add("Authenticator Key", (await userManager.GetAuthenticatorKeyAsync(user))!);
                var fileBytes = JsonSerializer.SerializeToUtf8Bytes(personalData);

                context.Response.Headers.TryAdd("Content-Disposition", "attachment; filename=PersonalData.json");
                return TypedResults.File(fileBytes, contentType: "application/json", fileDownloadName: "PersonalData.json");
            });

            return accountGroup;
        }
    }
}
