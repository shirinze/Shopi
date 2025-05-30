using Humanizer;
using Shopi.DomainModel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopi.Application.ViewModels;

public class EnumViewModel(Enum e)
{
    public int Id { get; } = (int)(object)e;
    public string Name { get; } = e.ToString();
    public string LastName { get; } = e.ToString();
    public string Phone { get; } = e.ToString();
    public string Description { get; } = e.Humanize();
    public Dictionary<string, object> Information { get; } = GetInformation(e);

    private static Dictionary<string, object> GetInformation(Enum e)
    {
        var dictionary = new Dictionary<string, object>();

        var enumType = e.GetType();
        var enumField = enumType.GetField(e.ToString());

        if (enumField is null)
            return dictionary;

        var infoAttributes = enumField.GetCustomAttributes(false).Where(attr => attr is InfoAttribute);

        dictionary = infoAttributes.Cast<InfoAttribute>()
        .ToDictionary(
            infoAttribute => infoAttribute.Name,
            infoAttribute => infoAttribute.Value);

        return dictionary;
    }

}
