using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer.Lifetime
{
    public class PerResolveLifetime : ILifetime
    {
        private readonly object key = new object();

        public object GetValue(ResolutionContext resolutionContext, Func<object> creator)
        {
            object value = resolutionContext.GetValue(key);
            if (value != null)
            {
                return value;
            }
            else
            {
                value = creator();
                resolutionContext.AddValue(key, value);
                return value;
            }            
        }
    }
}
