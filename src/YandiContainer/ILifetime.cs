﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer
{
    public interface ILifetime : IDisposable
    {
        Object GetValue();
        void RemoveValue();
        void SetValue(Object newValue);
    }
}
