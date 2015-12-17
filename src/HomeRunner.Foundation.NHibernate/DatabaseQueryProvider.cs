
using HomeRunner.Foundation.NHibernate.Contract;
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
