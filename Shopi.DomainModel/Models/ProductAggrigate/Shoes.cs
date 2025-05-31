using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopi.DomainModel.Models.ProductAggrigate;

public class Shoes : Product
{
    public int Size { get; set; }
    public string Color { get; set; } = string.Empty;
}
