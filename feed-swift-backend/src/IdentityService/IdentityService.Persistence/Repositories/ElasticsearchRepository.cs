using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Repositories;
using Nest;

namespace IdentityService.Persistence.Repositories;


public class ElasticsearchRepository<T> : IElasticsearchRepository<T> where T : BaseElasticSearchEntity
{
    private readonly IElasticClient _elasticClient;
    private readonly string _indexName;

    public ElasticsearchRepository(IElasticClient elasticClient, string indexName)
    {
        _elasticClient = elasticClient;
        _indexName = indexName;
    }

    public virtual async Task<ISearchResponse<T>> GetAsync()
    {
        return await _elasticClient.SearchAsync<T>();
    }

    public virtual async Task<GetResponse<T>> GetByIdAsync(string id)
    {
        return await _elasticClient.GetAsync<T>(DocumentPath<T>.Id(id), s => s.Index(_indexName));
    }

    public virtual async Task<IndexResponse> InsertAsync(T entity)
    {
        return await _elasticClient.IndexAsync(entity, ss => ss.Index(_indexName));
    }

    public virtual async Task<BulkResponse> InsertManyAsync(IEnumerable<T> entity)
    {
        return await _elasticClient.IndexManyAsync<T>(entity);
    }

    public virtual async Task<UpdateResponse<T>> UpdateAsync(string id, T entity)
    {
        return await _elasticClient.UpdateAsync(DocumentPath<T>.Id(id),
            ss => ss.Index(_indexName).Doc(entity).RetryOnConflict(3));
    }

    public virtual async Task<BulkResponse> UpdateManyAsync(IEnumerable<T> entity)
    {
        return await _elasticClient.BulkAsync(b => b.Index(_indexName).UpdateMany(entity, (b, e) => b.Doc(e)));
    }

    public virtual async Task<DeleteResponse> DeleteAsync(string id)
    {
        return await _elasticClient.DeleteAsync(DocumentPath<T>.Id(id), ss => ss.Index(_indexName));
    }

    public virtual async Task<ISearchResponse<T>> SearchAsync(SearchDescriptor<T> query)
    {
        return await _elasticClient.SearchAsync<T>(query);
    }
}