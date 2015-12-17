
using Dapper;
using HomeRunner.Foundation.Dapper.Filter;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HomeRunner.Foundation.Dapper
{
    /// <summary>
    /// The base class for NHibernate entity context implementations.
    /// </summary>
    public sealed class DapperEntityContext
        : EntityContext
    {
        private readonly IMappingProvider mappingProvider;

        /// <summary>
        /// TRUE if the object is disposed, FALSE otherwise.
        /// </summary>
        private bool isDisposed = false;

        private readonly IDbConnection connection;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DapperEntityContext(IMappingProvider mappingProvider) :
            base()
        {
            this.mappingProvider = mappingProvider;
            this.connection = DatabaseHelper.CreateDbConnection();
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~DapperEntityContext()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the entity identified by the identifier.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <typeparam name="TIdentifier">The entity identifier type.</typeparam>
        /// <param name="identifier">The entity identifier.</param>
        /// <returns>The entity matching the given identifier.</returns>
        public override TEntity Get<TEntity, TIdentifier>(TIdentifier identifier)
        {
            string query = string.Format("SELECT * FROM {0} WHERE ID = '{1}'", this.mappingProvider.Mappings[typeof(TEntity)], identifier);
            IEnumerable<TEntity> list = this.connection.Query<TEntity>(query);

            return list.SingleOrDefault();
        }

        /// <summary>
        /// Gets the entity filtered by the given <see cref="ICriteria"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="criteria">>The criteria for entity selection.</param>
        /// <returns>The entity matching the given criteria.</returns>
        public override TEntity Get<TEntity>(ICriteria criteria)
        {
            string query = string.Format("SELECT * FROM {0} WHERE {1}", this.mappingProvider.Mappings[typeof(TEntity)], criteria);
            IEnumerable<TEntity> list = this.connection.Query<TEntity>(query);

            return list.SingleOrDefault();
        }

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>The entity set of the given entity type.</returns>
        public override IList<TEntity> GetList<TEntity>()
        {
            string query = string.Format("SELECT * FROM {0} ", this.mappingProvider.Mappings[typeof(TEntity)]);
            IEnumerable<TEntity> entities = this.connection.Query<TEntity>(query);

            return entities.ToList();
        }

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>The entity set of the given entity type.</returns>
        public override IList<TEntity> GetList<TEntity>(ICriteria criteria)
        {
            string query = string.Format("SELECT * FROM {0} WHERE {1}", this.mappingProvider.Mappings[typeof(TEntity)], criteria);
            IEnumerable<TEntity> entities = this.connection.Query<TEntity>(query);

            return entities.ToList();
        }

        #region - Private & protected methods -

        /// <summary>
        /// Disposes the managed and unmanaged resources.
        /// </summary>
        /// <param name="isDisposing">TRUE if disposing and managed resources should be disposed, FALSE otherwise.</param>
        protected override void Dispose(bool isDisposing)
        {
            if (!this.isDisposed)
            {
                if (isDisposing)
                {
                    if (connection != default(IDbConnection))
                    {
                        connection.Close();
                    }
                }
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
