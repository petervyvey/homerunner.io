
namespace HomeRunner.Foundation.Entity
{
    /// <summary>
    /// Extends functionality on entities.
    /// </summary>
    public static class EntityExtension
    {
        /// <summary>
        /// Returns TRUE if the entity is in transtient state, return FALSE otherwise.
        /// </summary>
        /// <returns>TRUE if the entity is in TRANSIENT state, FALSE otherwise.</returns>
        public static bool IsTransient<TIdentifier>(this IIdentifiable<TIdentifier> entity)
        {
            return entity.Id.Equals(default(TIdentifier));
        }
    }
}
