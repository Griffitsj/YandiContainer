using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer.Registration
{
    public class LambdaFactory<T> : IFactory
    {
        Func<Container, T> factory;

        public LambdaFactory(Func<Container, T> factory)
        {
            this.factory = factory;
        }

        public object CreateObject(Container container)
        {
            return this.factory(container);
        }

        public void Dispose()
        {
            this.factory = null;
        }
    }
}
