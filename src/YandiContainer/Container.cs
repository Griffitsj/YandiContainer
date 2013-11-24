using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YandiContainer.Lifetime;
using YandiContainer.Registration;

namespace YandiContainer
{
    public sealed class Container : IDisposable
    {
        // TODO: 
        //  1. Consider concurrency ?
        //  2. Register container in the container   
        //  3. RegistrationEntry dependencies have too many duplicated type properties.
        //  4. Register lifetimes and factories in the container?
        //  5. Child containers - create separate ChildContainer class?
        //  6. More Tests
        //  7. High performance DefaultFactory
        RegistrationRepository repository = new RegistrationRepository();

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
            this.repository.AddRegistrationEntry(type, entry);
        }

        private RegistrationEntry AddDefaultRegistrationEntry(Type type)
        {
            var registrationEntry = new RegistrationEntry(type);
            this.repository.AddRegistrationEntry(type, registrationEntry);
            return registrationEntry;
        }

        public object Resolve(Type type)
        {
            RegistrationEntry registrationEntry = this.repository.GetRegistrationEntry(type);
            if (registrationEntry != null)            
            {
                return ResolveRegistrationEntry(registrationEntry);
            }
            else
            {
                registrationEntry = this.AddDefaultRegistrationEntry(type);
                return ResolveRegistrationEntry(registrationEntry);
            }
        }

        private object ResolveRegistrationEntry(RegistrationEntry registrationEntry)
        {
            var value = registrationEntry.Lifetime.GetValue();
            if (value != null)
            {
                return value;
            }
            else
            {
                value = registrationEntry.Factory.CreateObject(this);
                registrationEntry.Lifetime.SetValue(value);
                return value;
            }
        }

        public Container CreateChildContainer()
        {
            Container childContainer = new Container();
            childContainer.repository = this.repository;
            return childContainer;
        }

        public void Dispose()
        {
            foreach (var item in this.repository.GetAllRegistrations())
            {
                var disposable = item.Lifetime.GetValue() as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
