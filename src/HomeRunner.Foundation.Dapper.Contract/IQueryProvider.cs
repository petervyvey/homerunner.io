
using HomeRunner.Foundation.Dapper.Filter;

namespace HomeRunner.Foundation.Dapper
{
    public interface IQueryProvider
    {
        ICriteria<TEntity> From<TEntity>() where TEntity : class;

		ICriterion<TEntity> CreateCriterion<TEntity>(string prefix) where TEntity : class;
    }
}
