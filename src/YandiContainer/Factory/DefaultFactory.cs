using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace YandiContainer.Registration
{
    /// <summary>
    ///  ? Use Generics here ?
    /// </summary>
    public sealed class DefaultFactory : IFactory
    {
        private readonly Type type;

        public DefaultFactory(Type type)
        {
            this.type = type;

            // locate constructor to use
            // generate delegate
        }

        public object CreateObject(Container container, ResolutionContext resolutionContext)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (resolutionContext == null) throw new ArgumentNullException("resolutionContext");
            
            // resolve each of the ctor dependencies using container
            // instantiate object
            var ctor = GetMostComplexConstructor();

            List<object> parameters = new List<object>();

            foreach (var item in ctor.GetParameters())
            {
                parameters.Add(container.Resolve(item.ParameterType, resolutionContext));
            }

            return ctor.Invoke(parameters.ToArray());
        }

        private ConstructorInfo GetMostComplexConstructor()
        {
            return this.type.GetConstructors()
                .Where(p => p.IsPublic)
                .OrderByDescending(ci => ci.GetParameters().Length).First();
        }

        public void Dispose()
        {
            // dispose stuff that needs disposing.
        }
    }
}
