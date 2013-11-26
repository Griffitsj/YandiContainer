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
            var registrationEntry = new RegistrationEntry(typeof(Container), string.Empty, new ContainerLifetime(this));
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
            return this.Resolve(type, new ResolutionContext());
        }

        public object Resolve(Type type, ResolutionContext resolutionContext)
        {
            RegistrationEntry registrationEntry = this.repository.GetRegistrationEntry(type);
            if (registrationEntry != null)
            {
                return this.ResolveRegistrationEntry(registrationEntry, resolutionContext);
            }
            else
            {
                registrationEntry = this.AddDefaultRegistrationEntry(type);
                return this.ResolveRegistrationEntry(registrationEntry, resolutionContext);
            }
        }

        private object ResolveRegistrationEntry(RegistrationEntry registrationEntry, ResolutionContext resolutionContext)
        {
            var value = registrationEntry.Lifetime.GetValue(resolutionContext, () => registrationEntry.Factory.CreateObject(this, resolutionContext));
            return value;
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
                var disposableLifetime = item.Lifetime as IDisposable;
                if (disposableLifetime != null)
                {
                    disposableLifetime.Dispose();
                }
            }
        }
    }
}
