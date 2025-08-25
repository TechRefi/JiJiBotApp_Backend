using JiJiBotApp_Backend.DTOs.Model.Repositories;
using static JiJiBotApp_Backend.DTOs.SearchRequests.Repositories.RepositoryRequest;

namespace JiJiBotApp_Backend.Repositories.Repositories
{
    public interface IRepositoriesRepository
    {
        Task<(IEnumerable<RepositoriesListModel> records, int totalCount)> SearchRepository(SearchRepositoryRequest request);
        Task<(int repositoryId, int totalCount)> AddRepository(AddRepositoryRequest request);
        Task<(bool result, int totalCount)> DeleteRepository(DeleteRepositoryRequest request);
        Task<(int repositoryId, int totalCount)> EditRepository(UpdateRepositoryRequest request); 
    }
}
