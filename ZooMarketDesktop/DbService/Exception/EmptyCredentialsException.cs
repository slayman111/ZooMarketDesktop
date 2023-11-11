namespace ZooMarketDesktop.DbService.Exception;

public class EmptyCredentialsException : System.Exception
{
    public EmptyCredentialsException() : base("Не указан логин или пароль")
    {
    }
}
