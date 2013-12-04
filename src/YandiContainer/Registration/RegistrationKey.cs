using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YandiContainer.Registration
{
    internal sealed class RegistrationKey : IEquatable<RegistrationKey>
    {
        private readonly string name;
        private readonly Type type;

        ////public RegistrationKey(Type type)
        ////{
        ////    this.name = string.Empty;
        ////    this.type = type;
        ////}

        public RegistrationKey(Type type, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            this.name = name;
            this.type = type;
        }

        public string Name
        {
            get { return name; }
        }

        public Type Type
        {
            get { return type; }
        }

        public override int GetHashCode()
        {
            return this.Type.GetHashCode() ^ this.Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as RegistrationKey);
        }


        public bool Equals(RegistrationKey other)
        {
            return other != null ? other.Name == this.Name && other.Type == this.Type : false;
        }
    }
}
