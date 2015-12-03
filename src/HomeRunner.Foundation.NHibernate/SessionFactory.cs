
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Configuration;
using System.Reflection;
using Configuration = NHibernate.Cfg.Configuration;

namespace HomeRunner.Foundation.NHibernate
{
    public class SessionFactory
    {
        private static ISessionFactory FACTORY;

        private static object LOCK = new object();

        //private IMediator mediator;

        //public SessionFactory(IMediator mediator)
        //{
        //    this.mediator = mediator;
        //}

        public ISession OpenSession()
        {
            if (FACTORY == null) //not threadsafe
            {
                lock (LOCK)
                {
                    if (FACTORY == null)
                    {
                        FACTORY = CreateSessionFactory();
                    }
                }
            }

            return FACTORY.OpenSession();
        }

        /// <summary>
        /// The session factory.
        /// </summary>
        /// <returns>A session factory.</returns>
        protected ISessionFactory CreateSessionFactory()
        {
            IPersistenceConfigurer persistence = null;

            switch (ConfigurationManager.AppSettings["nhibernate.dialect"])
            {
                case "MsSql2005":
                {
                    string connectionString = ConfigurationManager.AppSettings["nhibernate.connectionstring"];
                    persistence = MsSqlConfiguration.MsSql2005.ConnectionString(connectionString);
                }
                    break;

                case "MsSql2008":
                {
                    string connectionString = ConfigurationManager.AppSettings["nhibernate.connectionstring"];
                    persistence = MsSqlConfiguration.MsSql2008.ConnectionString(connectionString);
                }
                    break;
                 
                default:
                    throw new NotImplementedException();
            }

            Configuration configuration = new Configuration();
            //configuration.SetInterceptor(new NhibernateSqlInterceptor(this.mediator));
            configuration.SetInterceptor(new NhibernateSqlInterceptor());

            string mappingAssembly = ConfigurationManager.AppSettings["nhibernate.mappingAssembly"];

            FluentConfiguration fluentConfiguration =
                Fluently.Configure(configuration)
                    .Database(persistence)
                    .Mappings(mappings => mappings.FluentMappings.AddFromAssembly(Assembly.Load(mappingAssembly)));

            return fluentConfiguration.BuildSessionFactory();
        }
    }
}
