
using Shopi.DomainModel.BaseModels;

namespace Shopi.DomainModel.Models;

public class City:BaseEntity
{
    public string Name { get; set; } = string.Empty;
}
