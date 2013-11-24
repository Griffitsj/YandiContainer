using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer.Registration
{
    internal sealed class RegistrationRepository
    {
        Dictionary<RegistrationKey, RegistrationEntry> registrations = new Dictionary<RegistrationKey, RegistrationEntry>();

        //public RegistrationRepository(RegistrationRepository repository)
        //{
        //    throw new NotImplementedException();
        //    //foreach (var item in repository.registrations)
        //    //{
        //    //    // Note: This will use same lifetime instance - which is wrong
        //    //    registrations.Add(item.Key, item.Value);
        //    //}
        //}

        public void AddRegistrationEntry(Type type, RegistrationEntry entry)
        {
            this.AddRegistrationEntry(type, string.Empty, entry);
        }

        public void AddRegistrationEntry(Type type, string name, RegistrationEntry entry)
        {
            this.registrations.Add(new RegistrationKey(type), entry);
        }

        public RegistrationEntry GetRegistrationEntry(Type type)
        {
            return this.GetRegistrationEntry(type, string.Empty);
        }

        public RegistrationEntry GetRegistrationEntry(Type type, string name)
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

        public IEnumerable<RegistrationEntry> GetAllRegistrations()
        {
            return this.registrations.Values;
        }
    }
}
