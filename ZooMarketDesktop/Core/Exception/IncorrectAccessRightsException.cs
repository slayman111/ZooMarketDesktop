namespace ZooMarketDesktop.Exception;

internal class IncorrectAccessRightsException : System.Exception
{
    public IncorrectAccessRightsException() : base("Ошибка прав доступа")
    {
    }
}
