using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer.Lifetime
{
    public class PerResolveLifetime : ILifetime
    {
        private readonly object key = new object();

        public object GetValue(ResolutionContext context, Func<object> creator)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (creator == null) throw new ArgumentNullException("creator");

            object value = context.GetValue(key);
            if (value != null)
            {
                return value;
            }
            else
            {
                value = creator();
                context.AddValue(key, value);
                return value;
            }            
        }
    }
}
