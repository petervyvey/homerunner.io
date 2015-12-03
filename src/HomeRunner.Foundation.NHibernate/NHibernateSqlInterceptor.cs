
using NHibernate;
using NHibernate.SqlCommand;

namespace HomeRunner.Foundation.NHibernate
{
    /// <summary>
    /// The NHibernate <see cref="IInterceptor"/> that intercepts the executed SQL statement.
    /// </summary>
    public class NhibernateSqlInterceptor
        : EmptyInterceptor, IInterceptor
    {
        //private readonly IMediator mediator;

        //public NhibernateSqlInterceptor(IMediator mediator)
        //{
        //    this.mediator = mediator;
        //}

        SqlString IInterceptor.OnPrepareStatement(SqlString sql)
        {
            NHibernateSql.LastSqlStatement = sql.ToString();
            return sql;
        }

        bool IInterceptor.OnSave(object entity, object id, object[] state, string[] propertyNames, global::NHibernate.Type.IType[] types)
        {
            var test = entity;
            return true;
        }

        bool IInterceptor.OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, global::NHibernate.Type.IType[] types)
        {
            //if (entity is IWithDomainEvents)
            //{
            //    var entityWithEvents = entity as IWithDomainEvents;
            //    entityWithEvents.DomainEvents.ToList()
            //        .ForEach(de => 
            //            this.mediator.Publish(de));
            //}


            return true;
        }
    }
}
