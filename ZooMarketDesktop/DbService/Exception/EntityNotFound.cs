namespace ZooMarketDesktop.DbService.Exception;

public class EntityNotFound : System.Exception
{
    public EntityNotFound() : base($"Сущность не найдена")
    {
    }
}
