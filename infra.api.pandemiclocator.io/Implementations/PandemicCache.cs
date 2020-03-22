using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using infra.api.pandemiclocator.io.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace infra.api.pandemiclocator.io.Implementations
{
    public class PandemicCache : IPandemicCache
    {
        private readonly IDistributedCache _cache;
        public PandemicCache(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<bool> SetCacheAsync<T>(string key, T item, CancellationToken cancellationToken) where T : class
        {
            key = $"{typeof(T).Name}.{key}";
            if (string.IsNullOrEmpty(key) || item == null)
            {
                return false;
            }

            try
            {
                var itemJson = JsonSerializer.Serialize(item);
                if (string.IsNullOrEmpty(itemJson))
                {
                    return false;
                }

                await _cache.SetStringAsync(key, 
                    itemJson, 
                    new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                        SlidingExpiration = TimeSpan.FromMinutes(15)
                    }, 
                    cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                //TODO: log

                return false;
            }
        }

        public async Task<T> GetCacheAsync<T>(string key, CancellationToken cancellationToken) where T : class
        {
            key = $"{typeof(T).Name}.{key}";
            T itemPoco = null;
            var itemJson = await _cache.GetStringAsync(key, cancellationToken);
            if (!string.IsNullOrEmpty(itemJson))
            {
                try
                {
                    itemPoco = JsonSerializer.Deserialize<T>(itemJson);
                    return itemPoco;
                }
                catch(Exception ex)
                {
                    //TODO: log

                    //Itens com problema de deserialização devem ser eliminados
                    await _cache.RemoveAsync(key, cancellationToken);
                }
            }

            return itemPoco;
        }
    }
}
