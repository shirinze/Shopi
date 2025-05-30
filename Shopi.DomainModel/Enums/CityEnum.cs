using Shopi.DomainModel.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopi.DomainModel.Enums;

[EnumEndPoint("/CityEnums")]
public enum CityEnum
{
    [Description("تهران")]
    [Info("textColor", "#000000")]
    [Info("backgroundColor", "#FFFFFF")]
    [Info("borderColor", "#CCCCCC")]
    Tehran = 1,

    [Description("اصفهان")]
    [Info("textColor", "#FFFFFF")]
    [Info("backgroundColor", "#123456")]
    [Info("borderColor", "#654321")]
    Mashahd = 2,

    [Description("شیراز")]
    [Info("borderColor", "#654321")]
    Tabriz = 3,

    [Description("مشهد")]
    Shiraz = 4,

    [Description("تبریز")]
    Ahvaz = 5
}
