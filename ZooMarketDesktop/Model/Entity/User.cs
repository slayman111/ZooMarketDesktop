using System.Collections.Generic;

namespace ZooMarketDesktop.Model.Entity;

public class User
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Lastname { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Role Role { get; set; } = null!;
}
