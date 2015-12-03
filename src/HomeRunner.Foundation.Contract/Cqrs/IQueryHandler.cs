
using MediatR;

namespace HomeRunner.Foundation.Cqrs
{
    public interface IQueryHandler<in TQuery, out TQueryResult>
        : IRequestHandler<TQuery, TQueryResult> 
        where TQuery : IQuery<TQueryResult>
        where TQueryResult : class { }
}
