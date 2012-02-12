using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NHibernateHierarchy.Entities
{
    public class Organisation
    {
        protected readonly ICollection<Organisation> _subItems;

        protected Organisation()
        {
            _subItems = new Collection<Organisation>();
        }

        public Organisation(string name) : this()
        {
            Name = name;
        }

        public Organisation(string name, Organisation parent): this(name)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            parent.AddSubItem(this);
        }

        public virtual Guid? Id { get; protected set; }
        public virtual string Name { get; set; }

        public virtual IEnumerable<Organisation> SubItems
        {
            get { return _subItems; }
        }

        public virtual void AddSubItem(Organisation subItem)
        {
            subItem.Parent = this;
            _subItems.Add(subItem);
        }

        public virtual Organisation Parent { get; protected set; }
    }
}