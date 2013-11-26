using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer.Registration
{
    internal sealed class RegistrationRepository
    {
        private object syncObject = new object();        
        Dictionary<RegistrationKey, RegistrationEntry> registrations = new Dictionary<RegistrationKey, RegistrationEntry>();

        public void AddRegistrationEntry(Type type, RegistrationEntry entry)
        {
            this.AddRegistrationEntry(type, string.Empty, entry);
        }

        public void AddRegistrationEntry(Type type, string name, RegistrationEntry entry)
        {
            lock (this.syncObject)
            {
                this.registrations.Add(new RegistrationKey(type), entry);
            }
        }

        public RegistrationEntry GetRegistrationEntry(Type type)
        {
            return this.GetRegistrationEntry(type, string.Empty);
        }

        public RegistrationEntry GetRegistrationEntry(Type type, string name)
        {
            lock (this.syncObject)
            {
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
        }

        public IEnumerable<RegistrationEntry> GetAllRegistrations()
        {
            lock (this.syncObject)
            {
                return this.registrations.Values.ToList();
            }
        }
    }
}
