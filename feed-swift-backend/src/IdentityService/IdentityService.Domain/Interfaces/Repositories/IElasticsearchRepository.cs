using IdentityService.Domain.Entities;
using Nest;

namespace IdentityService.Domain.Interfaces.Repositories;

public interface IElasticsearchRepository<T> where T : BaseElasticSearchEntity
{
    Task<ISearchResponse<T>> GetAsync();
    Task<GetResponse<T>> GetByIdAsync(string id);
    Task<IndexResponse> InsertAsync(T entity);
    Task<BulkResponse> InsertManyAsync(IEnumerable<T> entity);
    Task<UpdateResponse<T>> UpdateAsync(string id, T entity);
    Task<BulkResponse> UpdateManyAsync(IEnumerable<T> entity);
    Task<DeleteResponse> DeleteAsync(string id);
    Task<ISearchResponse<T>> SearchAsync(SearchDescriptor<T> query);
}