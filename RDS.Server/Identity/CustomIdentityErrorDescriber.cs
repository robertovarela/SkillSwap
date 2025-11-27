using Microsoft.AspNetCore.Identity;

namespace RDS.Server.Identity;

/// <summary>
/// Fornece traduções para as mensagens de erro padrão do ASP.NET Core Identity.
/// </summary>
public class CustomIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() 
        => new() { Code = nameof(DefaultError), Description = "Ocorreu um erro desconhecido." };

    public override IdentityError PasswordTooShort(int length) 
        => new() { Code = nameof(PasswordTooShort), Description = $"A senha deve ter no mínimo {length} caracteres." };

    public override IdentityError PasswordRequiresNonAlphanumeric() 
        => new() { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "A senha deve ter pelo menos um caractere não alfanumérico (ex: *, #, @, !)." };

    public override IdentityError PasswordRequiresDigit() 
        => new() { Code = nameof(PasswordRequiresDigit), Description = "A senha deve ter pelo menos um dígito ('0'-'9')." };

    public override IdentityError PasswordRequiresLower() 
        => new() { Code = nameof(PasswordRequiresLower), Description = "A senha deve ter pelo menos uma letra minúscula ('a'-'z')." };

    public override IdentityError PasswordRequiresUpper() 
        => new() { Code = nameof(PasswordRequiresUpper), Description = "A senha deve ter pelo menos uma letra maiúscula ('A'-'Z')." };

    public override IdentityError InvalidUserName(string? userName) 
        => new() { Code = nameof(InvalidUserName), Description = $"O nome de usuário '{userName}' é inválido. Use apenas letras e números." };

    public override IdentityError DuplicateUserName(string userName) 
        => new() { Code = nameof(DuplicateUserName), Description = $"O nome de usuário '{userName}' já está em uso." };

    public override IdentityError DuplicateEmail(string email) 
        => new() { Code = nameof(DuplicateEmail), Description = $"O e-mail '{email}' já está em uso." };

    // Você pode continuar sobrescrevendo outros métodos para traduzir mais mensagens conforme a necessidade.
}