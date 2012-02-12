using FluentNHibernate.Mapping;
using NHibernateHierarchy.Entities;

namespace NHibernateHierarchy.Mappings
{
    public class OrganisationMap : ClassMap<Organisation>
    {
        public OrganisationMap()
        {
            Id(o => o.Id).GeneratedBy.GuidComb();
            Map(o => o.Name).Not.Nullable().Length(1024);
            HasMany(x => x.SubItems).Cascade.AllDeleteOrphan().Inverse().KeyColumn("ParentId").ForeignKeyConstraintName("FK_Organisation_Parent");
            References(o => o.Parent).Column("ParentId").ForeignKey("FK_Organisation_Parent");
        }
    }
}