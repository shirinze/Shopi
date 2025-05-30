using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopi.DomainService.Proxies;

public interface ITrackingCodeProxy
{
    public Task<string> Get(CancellationToken cancellationToken);
}
