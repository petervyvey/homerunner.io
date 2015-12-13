
namespace HomeRunner.Foundation.Entity
{
	public interface IDomainEntity 
	{
		object GetDataEnitity();
		void SetDataEnitity(object dataEntity);
	}

    public interface IDomainEntity<TDataEntity>
		: IDomainEntity
    {
		TDataEntity GetDataEnitity();
        void SetDataEnitity(TDataEntity dataEntity);
    }
}
