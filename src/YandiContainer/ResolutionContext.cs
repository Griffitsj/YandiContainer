using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer
{
    public class ResolutionContext
    {
        private readonly Dictionary<object, object> data = new Dictionary<object, object>();

        public void AddValue(object key, object value)
        {
            this.data[key] = value;
        }

        public object GetValue(object key)
        {
            object value;
            if (this.data.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }
    }
}
