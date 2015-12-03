
using MediatR;

namespace HomeRunner.Foundation.Cqrs
{
    public interface IQuery { }

    public interface IQuery<out TResult>
        : IQuery, IRequest<TResult> where TResult : class { }
}