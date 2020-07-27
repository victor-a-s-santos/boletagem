using Solve.FinancialMarketIntegration.API.Areas.Tickets.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solve.FinancialMarketIntegration.API.Areas.WK.Infrastructure.Cache
{
    public struct CacheKey<T, I>
    {
        public Type Type { get; }
        public I Index { get; }
        public CacheKey(I index)
        {
            this.Type = typeof(T);
            this.Index = index;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType().IsAssignableFrom(this.GetType()))
            {
                return this == (CacheKey<T, I>)obj;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            hash ^= this.Type.GetHashCode();
            hash *= 257;
            hash ^= this.Index.GetHashCode();
            return hash;
        }

        public static bool operator ==(CacheKey<T, I> c1, CacheKey<T, I> c2)
        {
            return c2.Type.IsAssignableFrom(c1.Type) && c1.Index.Equals(c2.Index);
        }

        public static bool operator !=(CacheKey<T, I> c1, CacheKey<T, I> c2)
        {
            return !c2.Type.IsAssignableFrom(c1.Type) || !c1.Index.Equals(c2.Index);
        }
    }
}
