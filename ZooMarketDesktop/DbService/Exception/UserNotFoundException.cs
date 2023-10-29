namespace ZooMarketDesktop.DbService.Exception;

internal class UserNotFoundException : System.Exception
{
    public UserNotFoundException() : base("Неверный логин или пароль")
    {
    }
}
