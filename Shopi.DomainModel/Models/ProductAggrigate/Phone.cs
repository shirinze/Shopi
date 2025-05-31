using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopi.DomainModel.Models.ProductAggrigate;

public class Phone : Product
{
    public string Model { get; set; } = string.Empty;
}
