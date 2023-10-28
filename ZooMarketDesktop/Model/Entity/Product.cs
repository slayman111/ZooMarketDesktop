namespace ZooMarketDesktop.Model.Entity;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int BrandId { get; set; }

    public int Amount { get; set; }

    public int ProductTypeId { get; set; }

    public decimal Price { get; set; }

    public int CreatedById { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual User CreatedBy { get; set; } = null!;

    public virtual ProductType ProductType { get; set; } = null!;
}
