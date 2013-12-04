using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer.Registration
{
    internal sealed class RegistrationRepository
    {
        private readonly object syncObject = new object();
        private bool registrationsChanged = false;
        private Dictionary<RegistrationKey, RegistrationEntry> registrations = new Dictionary<RegistrationKey, RegistrationEntry>();
        private Dictionary<RegistrationKey, RegistrationEntry> updatedRegistrations = new Dictionary<RegistrationKey, RegistrationEntry>();
        private List<RegistrationEntry> allRegistrations = null;

        private void ApplyUpdatedRegistrations()
        {
            // This code is optimised to avoid any locks for the commonest situation where the registrations are setup
            // at startup and not modified from then on.
            lock (this.syncObject)
            {
                if (registrationsChanged)
                {
                    foreach (var item in this.registrations)
                    {
                        this.updatedRegistrations[item.Key] = item.Value;
                    }

                    this.registrations = this.updatedRegistrations;
                    this.updatedRegistrations = new Dictionary<RegistrationKey, RegistrationEntry>();
                    this.registrationsChanged = false;
                }
            }
        }

        public void AddRegistrationEntry(Type type, RegistrationEntry entry)
        {
            this.AddRegistrationEntry(type, string.Empty, entry);
        }

        public void AddRegistrationEntry(Type type, string name, RegistrationEntry entry)
        {
            lock (this.syncObject)
            {
                // New registrations are added to a different dictionary to avoid changing the registrations.
                this.updatedRegistrations[new RegistrationKey(type, name)] = entry;
                this.registrationsChanged = true;
            }
        }

        public RegistrationEntry GetRegistrationEntry(Type type)
        {
            return this.GetRegistrationEntry(type, string.Empty);
        }

        public RegistrationEntry GetRegistrationEntry(Type type, string name)
        {
            if (this.registrationsChanged)
            {
                ApplyUpdatedRegistrations();
            }

            RegistrationEntry entry;
            if (this.registrations.TryGetValue(new RegistrationKey(type, name), out entry))
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
            if (this.registrationsChanged)
            {
                this.ApplyUpdatedRegistrations();
            }

            var localAllRegistrations = this.allRegistrations;
            if (localAllRegistrations != null)
            {
                return localAllRegistrations;
            }
            else
            {
                localAllRegistrations = this.allRegistrations = this.registrations.Values.ToList();
                return localAllRegistrations;
            }
        }
    }
}
