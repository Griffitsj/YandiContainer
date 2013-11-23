using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YandiContainer.Lifetime;

namespace YandiContainer.Registration
{
    public sealed class RegistrationEntry
    {
        private readonly Type type;
        private readonly string name;
        private readonly ILifetime lifetime;
        private readonly IFactory factory;

        public RegistrationEntry(Type type)
            : this(type, string.Empty, new TransientLifetime())
        {
        }

        public RegistrationEntry(Type type, ILifetime lifetime)
            : this(type, string.Empty, lifetime)
        {
        }

        public RegistrationEntry(Type type, string name, ILifetime lifetime)
            : this(type, name, lifetime, new DefaultFactory(type))
        {
        }

        public RegistrationEntry(Type type, string name, ILifetime lifetime, IFactory factory)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            this.type = type;
            this.name = name;
            this.lifetime = lifetime;
            this.factory = factory;
        }

        public Type Type
        {
            get { return type; }
        }

        public string Name
        {
            get { return name; }
        }

        public ILifetime Lifetime
        {
            get { return lifetime; }
        }

        public IFactory Factory
        {
            get { return factory; }
        }
    }
}