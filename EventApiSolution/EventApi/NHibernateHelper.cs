using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Cfg;

namespace EventApi
{
    public static class NHibernateHelper
    {
        private static ISessionFactory? _sessionFactory;
        private static readonly object _lock = new();

        public static ISessionFactory SessionFactory(IConfiguration configuration)
        {
            if (_sessionFactory == null)
            {
                lock (_lock)
                {
                    if (_sessionFactory == null)
                    {
                        var cfg = new Configuration();

                        var connectionString = configuration.GetConnectionString("DefaultConnection");

                        cfg.DataBaseIntegration(db =>
                        {
                            db.ConnectionString = connectionString;
                            db.Driver<NHibernate.Driver.SQLite20Driver>();
                            db.Dialect<NHibernate.Dialect.SQLiteDialect>();
                            db.LogSqlInConsole = true;
                        });

                        cfg.AddResource("EventApi.Mappings.EventMap.hbm.xml", typeof(NHibernateHelper).Assembly);
                        cfg.AddResource("EventApi.Mappings.TicketSaleMap.hbm.xml", typeof(NHibernateHelper).Assembly);
                        _sessionFactory = cfg.BuildSessionFactory();
                    }
                }
            }
            return _sessionFactory;
        }
    }
}