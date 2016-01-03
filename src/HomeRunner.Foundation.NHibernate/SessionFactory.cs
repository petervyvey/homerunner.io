
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using HomeRunner.Foundation.Configuration;
using NHibernate;
using System;
using System.Configuration;
using System.Reflection;
using NHibernate_Configuration = NHibernate.Cfg.Configuration;

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

            switch (ConfigurationManager.AppSettings[AppSetting.NHIBERNATE_DIALECT])
            {
                case "MsSql2005":
                {
                    string connectionString = ConfigurationManager.AppSettings[AppSetting.NHIBERNATE_CONNECTIONSTRING];
                    persistence = MsSqlConfiguration.MsSql2005.ConnectionString(connectionString);
                }
                    break;

                case "MsSql2008":
                {
                    string connectionString = ConfigurationManager.AppSettings[AppSetting.NHIBERNATE_CONNECTIONSTRING];
                    persistence = MsSqlConfiguration.MsSql2008.ConnectionString(connectionString);
                }
                    break;

                case "MySql":
                    {
                        string connectionString = ConfigurationManager.AppSettings[AppSetting.NHIBERNATE_CONNECTIONSTRING];
                        persistence = MySQLConfiguration.Standard.ConnectionString(connectionString);
                    }
                    break;
                 
                default:
                    throw new NotImplementedException();
            }

            NHibernate_Configuration configuration = new NHibernate_Configuration();
            //configuration.SetInterceptor(new NhibernateSqlInterceptor(this.mediator));
            configuration.SetInterceptor(new NhibernateSqlInterceptor());

            string mappingAssembly = ConfigurationManager.AppSettings[AppSetting.NHIBERNATE_MAPPING_ASSEMBLY];

            FluentConfiguration fluentConfiguration =
                Fluently.Configure(configuration)
                    .Database(persistence)
                    .Mappings(mappings => mappings.FluentMappings.AddFromAssembly(Assembly.Load(mappingAssembly)));

            return fluentConfiguration.BuildSessionFactory();
        }
    }
}
