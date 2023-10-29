namespace ZooMarketDesktop.DbService.Exception;

public class UserNotFoundException : System.Exception
{
    public UserNotFoundException() : base("Неверный логин или пароль")
    {
    }
}
