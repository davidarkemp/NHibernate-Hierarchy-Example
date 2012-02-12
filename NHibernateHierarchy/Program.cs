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
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true,true,false));

            var sessionFactory = configuration.BuildSessionFactory();


            using (var session = sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var top = new Organisation("Top Level");
                
                top.AddChild(new Organisation("Child 1"));
                

                new Organisation("Child 2", top);

                session.Save(top);
                

                transaction.Commit();
            }

            using (var session = sessionFactory.OpenSession())
            using(var  transaction = session.BeginTransaction())
            {
                var topLevel = session.QueryOver<Organisation>().Where(o => o.Name == "Top Level").SingleOrDefault();

                foreach (var subItem in topLevel.Children)
                {
                    Console.WriteLine(subItem.Name);
                }

                transaction.Commit();
            }

            Console.ReadLine();
        }
    }
}
