
namespace HomeRunner.Foundation.Dapper.Filter
{
    public interface ICriteriaProvider
    {
        ICriteria CreateCriteria();

        ICriterion CreateCriterion();
    }
}
