using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer.Lifetime
{
    public class HierarchicalLifetime : ILifetime
    {
        public object GetValue(ResolutionContext context, Func<object> creator)
        {
            throw new NotImplementedException();
        }
    }
}
