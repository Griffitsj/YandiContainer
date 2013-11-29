using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer.Registration
{
    internal sealed class RegistrationRepository
    {
        private object syncObject = new object();
        private bool updatedRegistrationsChanged = false;
        private Dictionary<RegistrationKey, RegistrationEntry> registrations = new Dictionary<RegistrationKey, RegistrationEntry>();
        private Dictionary<RegistrationKey, RegistrationEntry> updatedRegistrations = new Dictionary<RegistrationKey, RegistrationEntry>();

        public void AddRegistrationEntry(Type type, RegistrationEntry entry)
        {
            this.AddRegistrationEntry(type, string.Empty, entry);
        }

        public void AddRegistrationEntry(Type type, string name, RegistrationEntry entry)
        {
            lock (this.syncObject)
            {
                this.updatedRegistrations[new RegistrationKey(type, name)] = entry;
                this.updatedRegistrationsChanged = true;
            }
        }

        public RegistrationEntry GetRegistrationEntry(Type type)
        {
            return this.GetRegistrationEntry(type, string.Empty);
        }

        public RegistrationEntry GetRegistrationEntry(Type type, string name)
        {
            if (this.updatedRegistrationsChanged)
            {
                // This code is optimised to avoid any locks for the commonest situation where the registrations are setup
                // at startup and not modified from then on.
                lock(this.syncObject)
                {
                    if (updatedRegistrationsChanged)
                    {
                        foreach (var item in this.registrations)
                        {
                            this.updatedRegistrations[item.Key] = item.Value;
                        }

                        this.registrations = this.updatedRegistrations;
                        this.updatedRegistrations = new Dictionary<RegistrationKey, RegistrationEntry>();
                        this.updatedRegistrationsChanged = false;
                    }
                }
            }

            RegistrationEntry entry;
            if (this.registrations.TryGetValue(new RegistrationKey(type), out entry))
            {
                return entry;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<RegistrationEntry> GetAllRegistrations()
        {
            return this.registrations.Values.ToList();
        }
    }
}
