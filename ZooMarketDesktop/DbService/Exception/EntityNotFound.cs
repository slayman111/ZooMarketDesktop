namespace ZooMarketDesktop.DbService.Exception;

internal class EntityNotFound : System.Exception
{
    public EntityNotFound() : base($"Сущность не найдена")
    {
    }
}
