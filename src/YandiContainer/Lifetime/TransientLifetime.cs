using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer.Lifetime
{
    public sealed class TransientLifetime : ILifetime
    {        
        public object GetValue()
        {
            return null;
        }

        public void RemoveValue()
        {            
        }

        public void SetValue(object newValue)
        {
        }

        public void Dispose()
        {
        }
    }
}
