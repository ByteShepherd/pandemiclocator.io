﻿using System.Threading;
using System.Threading.Tasks;

namespace pandemiclocator.io.abstractions.Cache
{
    public interface IRedisProvider
    {
        Task<bool> SetCacheAsync<T>(string key, T item, CancellationToken cancellationToken) where T : class;
        Task<T> GetCacheAsync<T>(string key, CancellationToken cancellationToken) where T : class;
    }
}