﻿using Shared.Common.Helper.ErrorsHandler;

namespace ElasticSearch.Abstractions;

public interface IElasticSearchService<T> where T : class
{
    /// <summary>
    /// Perform a full-text search
    /// </summary>
    /// <param name="filters"></param>
    /// <param name="indexName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result<IReadOnlyCollection<T>>> SearchAsync(Func<T, object> func, string value, string indexName, CancellationToken cancellationToken);

    /// <summary>
    /// Add or update a document
    /// </summary>
    /// <param name="model"></param>
    /// <param name="indexName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> AddOrUpdateAsync(string id, T model, string indexName, CancellationToken cancellationToken);
}
