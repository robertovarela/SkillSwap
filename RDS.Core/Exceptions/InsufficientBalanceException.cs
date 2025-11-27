namespace RDS.Core.Exceptions;

/// <summary>
/// Exceção lançada quando um profissional não tem saldo suficiente para uma operação.
/// </summary>
public class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException() { }
    public InsufficientBalanceException(string message) : base(message) { }
    public InsufficientBalanceException(string message, Exception inner) : base(message, inner) { }
}
