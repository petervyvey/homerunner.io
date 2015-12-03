
namespace HomeRunner.Foundation.Entity
{
    public interface IDataEntity { }

    public interface IDataEntity<TIdentifier>
        : IDataEntity, IIdentifiable<TIdentifier> { }
}
