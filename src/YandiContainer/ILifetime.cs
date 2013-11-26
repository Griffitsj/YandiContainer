using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer
{
    public interface ILifetime
    {
        Object GetValue(ResolutionContext context, Func<object> creator);
    }
}
