using JiJiBotApp_Backend.Data;
using JiJiBotApp_Backend.DTOs.Model.Repositories;
using JiJiBotApp_Backend.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;
using static JiJiBotApp_Backend.DTOs.SearchRequests.Repositories.RepositoryRequest;

namespace JiJiBotApp_Backend.Repositories.Repositories
{
    public class RepositoriesRepository(IStoredProcedureExecutor spExecutor, ILogger<RepositoriesRepository> logger) : IRepositoriesRepository
    {
        public async Task<(IEnumerable<RepositoriesListModel> records, int totalCount)> SearchRepository(SearchRepositoryRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@StartIndex", SqlDbType.Int) { Value = request.StartIndex },
                new SqlParameter("@EndIndex", SqlDbType.Int) { Value = request.EndIndex },
                new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

            try
            {
                var result = await spExecutor.ExecuteStoredProcedureAsync("Sp_Repositories", parameters);
                var repoList = result.ToList<RepositoriesListModel>();
                int totalCount = repoList.Count;
                return (repoList, totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Repository search stored procedure");
                throw;
            }
        }

        public async Task<(int repositoryId, int totalCount)> AddRepository(AddRepositoryRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@Repo_name", SqlDbType.VarChar, 150) { Value = request.repo_name },
                new SqlParameter("@Description", SqlDbType.VarChar, 500) { Value = (object?)request.Description ?? DBNull.Value },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = request.IsActive },
                new SqlParameter("@CreatedBy", SqlDbType.Int) { Value = request.CreatedBy },
                  new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

            try
            {
                var result = await spExecutor.ExecuteScalarStoredProcedureAsync("Sp_Repositories", parameters);
                //return (Convert.ToInt32(result.Rows[0]["NewRepositoryId"]), 1);
                return (Convert.ToInt32(result),1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL error executing Repository add stored procedure");
                return (-1, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Repository add stored procedure");
                throw;
            }
        }

        public async Task<(bool result, int totalCount)> DeleteRepository(DeleteRepositoryRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

            try
            {
                await spExecutor.ExecuteStoredProcedureAsync("Sp_Repositories", parameters);
                return (true, 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL error executing Repository delete stored procedure");
                return (false, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Repository delete stored procedure");
                throw;
            }
        }

        public async Task<(int repositoryId, int totalCount)> EditRepository(UpdateRepositoryRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                new SqlParameter("@repo_name", SqlDbType.VarChar, 150) { Value = request.repo_name },
                new SqlParameter("@Description", SqlDbType.VarChar, 500) { Value = (object?)request.Description ?? DBNull.Value },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = request.IsActive },
                new SqlParameter("@UpdatedBy", SqlDbType.Int) { Value = request.UpdatedBy },
                new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

            try
            {
                await spExecutor.ExecuteStoredProcedureAsync("Sp_Repositories", parameters);
                return (request.Id, 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL error executing Repository update stored procedure");
                return (-1, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Repository update stored procedure");
                throw;
            }
        }
    }
}
