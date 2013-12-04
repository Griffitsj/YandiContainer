using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer.Lifetime
{
    public sealed class ContainerLifetime : ILifetime, IDisposable
    {
        private readonly object syncObject = new object();
        private object value;

        public ContainerLifetime()
        {
        }

        public ContainerLifetime(object value)
        {
            this.value = value;
        }

        public object GetValue(ResolutionContext context, Func<object> creator)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (creator == null) throw new ArgumentNullException("creator");

            if (value != null)
            {
                return value;
            }
            else
            {
                lock (syncObject)
                {
                    if (value != null)
                    {
                        return value;
                    }
                    else
                    {
                        value = creator();
                        return value;
                    }
                }
            }
        }

        public void Dispose()
        {
            value.DisposeIfDisposable();
        }
    }
}
