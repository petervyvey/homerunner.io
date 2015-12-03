
using System.Collections.Generic;
using System.Linq;

namespace HomeRunner.Foundation.Entity
{
    public static class DomainEntityExtension
    {
        public static IEnumerable<TDomainEntity> ToDomainEntities<TDataEntity, TDomainEntity>(this IEnumerable<TDataEntity> entities)
            where TDataEntity : IDataEntity
            where TDomainEntity : IDomainEntity<TDataEntity>, new()
        {
            entities = entities.ToList();

            var count = entities.Count();
            for (int i = 0; i < count; i++)
            {
                yield return entities.ToArray()[i].ToDomainEntity<TDataEntity, TDomainEntity>();
            }
        }

        public static TDomainEntity ToDomainEntity<TDataEntity, TDomainEntity>(this TDataEntity entity)
            where TDataEntity : IDataEntity
            where TDomainEntity : IDomainEntity<TDataEntity>, new()
        {
            TDomainEntity domainEntity = new TDomainEntity();
            domainEntity.SetDataEnitity(entity);

            return domainEntity;
        }
    }
}