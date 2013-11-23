using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YandiContainer.Lifetime;
using YandiContainer.Registration;

namespace YandiContainer
{
    public class Container : IDisposable
    {
        // TODO: 
        //  1. Consider concurrency - use Concurrent Dictionary ?
        //  2. Register container in the container   
        //  3. RegistrationEntry dependencies have too many duplicated type properties.
        //  4. Register lifetimes and factories in the container?
        //  5. Child containers
        //  6. More Tests
        //  7. High performance DefaultFactory
        Dictionary<RegistrationKey, RegistrationEntry> registrations = new Dictionary<RegistrationKey, RegistrationEntry>();

        public Container()
        {
            AddDefaultRegistrations();
        }

        private void AddDefaultRegistrations()
        {
            var registrationEntry = new RegistrationEntry(typeof(Container), string.Empty, new ContainerLifetime());
            registrationEntry.Lifetime.SetValue(this);
            this.AddRegistrationEntry(typeof(Container), registrationEntry);
        }

        public void AddRegistrationEntry(Type type, RegistrationEntry entry)
        {
            this.registrations.Add(new RegistrationKey(type), entry);
        }

        private RegistrationEntry AddDefaultRegistrationEntry(Type type)
        {
            var registrationEntry = new RegistrationEntry(type, string.Empty, new TransientLifetime(), new DefaultFactory(type));
            this.registrations.Add(new RegistrationKey(type), registrationEntry);
            return registrationEntry;
        }

        public object Resolve(Type type)
        {
            RegistrationEntry re;

            if (this.registrations.TryGetValue(new RegistrationKey(type), out re))
            {
                return ResolveRegistrationEntry(re);
            }
            else
            {
                re = this.AddDefaultRegistrationEntry(type);
                return ResolveRegistrationEntry(re);
            }
        }

        private object ResolveRegistrationEntry(RegistrationEntry re)
        {
            var value = re.Lifetime.GetValue();
            if (value != null)
            {
                return value;
            }
            else
            {
                value = re.Factory.CreateObject(this);
                re.Lifetime.SetValue(value);
                return value;
            }
        }

        public void Dispose()
        {
            foreach (var item in this.registrations)
            {
                var disposable = item.Value.Lifetime.GetValue() as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }

                item.Value.Lifetime.RemoveValue();
            }

            this.registrations.Clear();
        }
    }
}
