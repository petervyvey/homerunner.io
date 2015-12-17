
using HomeRunner.Foundation.Dapper.Filter;
using System;
using System.Collections.Generic;

namespace HomeRunner.Foundation.Dapper
{
    /// <summary>
    /// Interface defining the API for the entity context.
    /// </summary>
    public interface IEntityContext
        : IDisposable 
    {
        /// <summary>
        /// Gets the entity identified by the identifier.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <typeparam name="TIdentifier">The entity identifier type.</typeparam>
        /// <param name="identifier">The entity identifier.</param>
        /// <returns>The entity matching the given identifier.</returns>
        TEntity Get<TEntity, TIdentifier>(TIdentifier identifier);

        /// <summary>
        /// Gets the entity filtered by the given <see cref="ICriteria"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="criteria">>The criteria for entity selection.</param>
        /// <returns>The entity matching the given criteria.</returns>
        TEntity Get<TEntity>(ICriteria criteria);

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>The entity set of the given entity type.</returns>
        IList<TEntity> GetList<TEntity>() where TEntity : class;

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="criteria">Criteria for entity selection.</param>
        /// <returns>The entity set of the given entity type.</returns>
        IList<TEntity> GetList<TEntity>(ICriteria criteria) where TEntity : class;
    }
}
