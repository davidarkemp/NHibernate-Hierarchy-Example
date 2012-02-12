using System;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using NHibernateHierarchy.Entities;

namespace NHibernateHierarchy
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = 
                Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(s => s.FromConnectionStringWithKey("DB")).ShowSql)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>().ExportTo(Console.Out))
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true,true))
                ;

            var sessionFactory = configuration.BuildSessionFactory();


            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var top = new Organisation("Top Level");
                
                foreach (var name in Enumerable.Range(1,10).Select(i => "Child " + i))
                {
                    top.SubItems.Add(new Organisation(name, parent:top));
                }

                session.Save(top);
                

                transaction.Commit();
            }

            Console.ReadLine();
        }
    }
}
