
namespace HomeRunner.Foundation.Entity
{
    public interface IDomainEntity<TDataEntity>
    {
        void SetDataEnitity(TDataEntity dataEntity);
    }
}
