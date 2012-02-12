using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NHibernateHierarchy.Entities
{
    public class Organisation
    {
        protected readonly ICollection<Organisation> _children;

        protected Organisation()
        {
            _children = new Collection<Organisation>();
        }

        public Organisation(string name) : this()
        {
            Name = name;
        }

        public Organisation(string name, Organisation parent): this(name)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            parent.AddChild(this);
        }

        public virtual Guid? Id { get; protected set; }
        public virtual string Name { get; set; }

        public virtual IEnumerable<Organisation> Children
        {
            get { return _children; }
        }

        public virtual void AddChild(Organisation child)
        {
            child.Parent = this;
            _children.Add(child);
        }

        public virtual Organisation Parent { get; protected set; }
    }
}