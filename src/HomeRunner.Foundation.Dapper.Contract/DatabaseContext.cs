
using HomeRunner.Foundation.Dapper.Filter;
using System;
using System.Collections.Generic;

namespace HomeRunner.Foundation.Dapper
{
    /// <summary>
    /// The base class of persistence context implementations.
    /// </summary>
    public abstract class DatabaseContext
        : IDatabaseContext
    {
        /// <summary>
        /// TRUE if the object is disposed, FALSE otherwise.
        /// </summary>
        private bool isDisposed = false;

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected DatabaseContext()
            : base() { }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~DatabaseContext()
        {
            this.Dispose(false);
        }

		/// <summary>
		/// Gets the mappings.
		/// </summary>
		/// <value>The mappings.</value>
		public abstract Dictionary<Type, string> Mappings { get; }

        /// <summary>
        /// Gets the entity identified by the identifier.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <typeparam name="TIdentifier">The entity identifier type.</typeparam>
        /// <param name="identifier">The entity identifier.</param>
        /// <returns>The entity matching the given identifier.</returns>
        public abstract TEntity Get<TEntity, TIdentifier>(TIdentifier identifier) where TEntity : class;

        /// <summary>
        /// Gets the entity filtered by the given <see cref="ICriteria"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="criteria">>The criteria for entity selection.</param>
        /// <returns>The entity matching the given criteria.</returns>
        public abstract TEntity Get<TEntity>(ICriteria criteria) where TEntity : class;

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>The entity set of the given entity type.</returns>
        public abstract IList<TEntity> GetList<TEntity>() where TEntity : class;

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="criteria">Criteria for entity selection.</param>
        /// <returns>The entity set of the given entity type.</returns>
        public abstract IList<TEntity> GetList<TEntity>(ICriteria criteria) where TEntity : class;

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            if (!this.isDisposed)
            {
                this.Dispose(true);
            }
            GC.SuppressFinalize(this);
        }

        #region - Private & protected methods -

        /// <summary>
        /// Disposes managed and unmanaged resources.
        /// </summary>
        /// <param name="isDisposing">Flag indicating how this protected method was called. 
        /// TRUE means via Dispose(), FALSE means via the destructor.
        /// Only in case of a call through the Dispose() method should managed resources be freed.</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (!this.isDisposed) { }

            this.isDisposed = true;
        }

        #endregion
    }
}
