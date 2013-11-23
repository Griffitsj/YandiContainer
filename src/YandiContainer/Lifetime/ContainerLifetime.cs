using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer.Lifetime
{
    public sealed class ContainerLifetime : ILifetime
    {
        private object value;
        
        public object GetValue()
        {
            return value;
        }

        public void RemoveValue()
        {
            value = null;
        }

        public void SetValue(object newValue)
        {
            value = newValue;
        }

        public void Dispose()
        {
            var disposable = this.value as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
            this.value = null;
        }
    }
}
