﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer.Lifetime
{
    public sealed class TransientLifetime : ILifetime
    {        
        public object GetValue(ResolutionContext context, Func<object> creator)
        {
            return creator();
        }
    }
}
