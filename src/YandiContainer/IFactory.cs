using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer.Registration
{
    public interface IFactory : IDisposable
    {
        object CreateObject(Container container, ResolutionContext resolutionContext);
    }
}
