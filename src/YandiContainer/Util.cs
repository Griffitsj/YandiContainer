using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer
{
    public static class Extensions
    {
        public static void DisposeIfDisposable(this object value)
        {
            if (value == null)
            {
                return;
            }

            var disposable = value as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
