using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer.Registration
{
    public class LambdaFactory<T> : IFactory
    {
        Func<Container, ResolutionContext, T> factory;

        public LambdaFactory(Func<Container, ResolutionContext, T> factory)
        {
            this.factory = factory;
        }

        public object CreateObject(Container container, ResolutionContext resolutionContext)
        {
            return this.factory(container, resolutionContext);
        }

        public void Dispose()
        {
            this.factory = null;
        }
    }
}
