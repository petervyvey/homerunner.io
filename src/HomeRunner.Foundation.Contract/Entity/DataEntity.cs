
namespace HomeRunner.Foundation.Entity
{
    /// <summary>
    /// Abstract base class for data entities.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public abstract class DataEntity<TIdentifier>
        : IDataEntity<TIdentifier>
    {
        /// <summary>
        /// The entity identifier.
        /// </summary>
        public virtual TIdentifier Id { get; set; }
    }
}
