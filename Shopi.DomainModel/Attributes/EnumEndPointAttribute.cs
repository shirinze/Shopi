
namespace Shopi.DomainModel.Attributes;

[AttributeUsage(AttributeTargets.Enum)]
public class EnumEndPointAttribute(string route):Attribute
{
    public string Route { get; set; } = route;
}
