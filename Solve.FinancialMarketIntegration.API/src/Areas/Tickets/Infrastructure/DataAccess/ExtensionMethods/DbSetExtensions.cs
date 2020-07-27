using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Solve.FinancialMarketIntegration.API.Areas.Tickets.Entities;

public static class DbSetExtensions
{
            /// <summary>
        /// Get record from cache or attach
        /// </summary>
        /// <typeparam name="T">T used in DbSet</typeparam>
        /// <param name="collection">DbSet</param>
        /// <param name="searchLocalQuery">Criteria to search record in DbSet</param>
        /// <param name="getAttachItem">Function to attach new data in DbSet</param>
        /// <example>
        /// var country = Context.Countries.GetLocalOrAttach(c => c.Id == CountryId, () => new Country { Id = CountryId });
        /// </example>
        /// <returns>
        /// Return data from cache or new data attached
        /// </returns>
        public static T GetLocalOrAttach<T>(this DbSet<T> collection, Func<T, bool> searchLocalQuery, Func<T> getAttachItem) 
            where T : class
        {
            T localEntity = collection.Local.FirstOrDefault(searchLocalQuery);

            if (localEntity == null)
            {
                localEntity = getAttachItem?.Invoke();
                collection.Attach(localEntity);
            }

            return localEntity;
        }

        public static TEntity GetLocalOrAttach<TEntity, TKey>(this DbSet<TEntity> collection, TKey key)
            where TEntity : Entity<TKey>, new()
            where TKey: struct
        {
            var localEntity = collection.Local.FirstOrDefault(i => i.Id.Equals(key));

            if (localEntity == null)
            {
                localEntity = new TEntity() { Id = key };
                collection.Attach(localEntity);
            }

            return localEntity;
        }

}