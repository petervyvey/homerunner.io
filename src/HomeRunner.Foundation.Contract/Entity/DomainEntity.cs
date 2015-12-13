
using System.Collections.Generic;
using HomeRunner.Foundation.Cqrs;

namespace HomeRunner.Foundation.Entity
{
    /// <summary>
    /// Abstract base class for domain entities.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TDataEntity">The <see cref="IDataEntity"/> type.</typeparam>
    public abstract class DomainEntity<TIdentifier, TDataEntity>
        : IDomainEntity<TDataEntity>, IIdentifiable<TIdentifier>, IWithDomainEvents
        where TDataEntity : IDataEntity<TIdentifier>
    {
        protected TDataEntity entity = default(TDataEntity);

        protected DomainEntity()
        {
            this.DomainEvents = new List<IDomainEvent>();
        }

        /// <summary>
        /// The entity identifier.
        /// </summary>
        public TIdentifier Id
        {
            get { return this.entity.Id; } 
            set { this.entity.Id = value; }
        }

        public List<IDomainEvent> DomainEvents { get; private set; }

		object IDomainEntity.GetDataEnitity()
		{
			return this.entity;
		}

		public TDataEntity GetDataEnitity()
		{
			return this.entity;
		}

		void IDomainEntity.SetDataEnitity(object dataEntity)
		{
			this.entity = (TDataEntity)dataEntity;
		}

        public void SetDataEnitity(TDataEntity dataEntity)
        {
            this.entity = dataEntity;
        }
    }
}
