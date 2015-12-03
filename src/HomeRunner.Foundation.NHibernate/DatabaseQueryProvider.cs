
using HomeRunner.Foundation.Infrastructure;
using NHibernate;

namespace HomeRunner.Foundation.NHibernate
{
    public sealed class DatabaseQueryProvider
        : QueryProvider<ISession>
    {
        public DatabaseQueryProvider(ISession session) 
            : base(session) { }
    }
}
