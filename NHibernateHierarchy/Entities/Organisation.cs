using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NHibernateHierarchy.Entities
{
    public class Organisation
    {
        protected Organisation()
        {
            SubItems = new Collection<Organisation>();
        }

        public Organisation(string name, Organisation parent=null) : this()
        {
            Name = name;
            if (parent == null) return;
            parent.SubItems.Add(this);
            Parent = parent;

        }

        public virtual Guid? Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual ICollection<Organisation> SubItems { get; protected set; }

        public virtual Organisation Parent { get; protected set; }
    }
}