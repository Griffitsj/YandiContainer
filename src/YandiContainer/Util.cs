using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer
{
    public static class Util
    {
        public static void DisposeIfDisposable(this object obj)
        {
            if (obj == null)
            {
                return;
            }

            var disposable = obj as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
