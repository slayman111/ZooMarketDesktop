using System.Collections.Generic;

namespace ZooMarketDesktop.Model.Entity;

public class ProductType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
