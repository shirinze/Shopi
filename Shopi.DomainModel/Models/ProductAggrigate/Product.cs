using Shopi.DomainModel.BaseModels;

namespace Shopi.DomainModel.Models.ProductAggrigate;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
