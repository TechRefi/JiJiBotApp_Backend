using JiJiBotApp_Backend.Data;
using JiJiBotApp_Backend.DTOs.Model.UserRepoAssociation;
using JiJiBotApp_Backend.DTOs.SearchRequests.UserRepoAssociation;
using JiJiBotApp_Backend.Extensions;
using JiJiBotApp_Backend.Services.UserRepoAssociation;
using Microsoft.Data.SqlClient;
using System.Data;

namespace JiJiBotApp_Backend.Repositories.UserRepoAssociation
{
    public class UserRepoAssociationRepository(IStoredProcedureExecutor spExecutor, ILogger<UserRepoAssociationRepository> logger) :
        IUserRepoAssociationRepository
    {
        public async Task<(IEnumerable<UserRepoAssociationListModel> records, int totalCount)> SearchUser(UserRepoAssociationSearchRequest request)
        {
            var parameters = new[] {
                new SqlParameter("@StartIndex", SqlDbType.Int) { Value = request.StartIndex },
                new SqlParameter("@EndIndex", SqlDbType.Int) { Value = request.EndIndex },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
            try
            {
                var result = await spExecutor.ExecuteStoredProcedureAsync("sp_UserRepoAssociation", parameters);
                var list = result.ToList<UserRepoAssociationListModel>();
                int totalCount = list.Count;
                return (list, totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing UserRepoAssociation search stored procedure");
                throw;
            }
        }

        public async Task<(int Id, int totalCount)> AddUser(UserRepoAssociationAddRequest request)
        {
            var parameters = new[] {
                new SqlParameter("@UserId", SqlDbType.Int) { Value = request.UserId },
                new SqlParameter("@RepoId", SqlDbType.Int) { Value = request.RepoId },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = request.IsActive },
                new SqlParameter("@CreatedBy", SqlDbType.Int) { Value = request.CreatedBy },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
            try
            {
                var result = await spExecutor.ExecuteStoredProcedureAsync("sp_UserRepoAssociation", parameters);
                return (Convert.ToInt32(result.Rows[0]["NewUserRepoAssociationId"]), 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Error executing UserRepoAssociation add stored procedure");
                return (-1, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing UserRepoAssociation add stored procedure");
                throw;
            }
        }

        public async Task<(bool result, int totalCount)> DeleteUser(UserRepoAssociationDeleteRequest request)
        {
            var parameters = new[] {
                new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
            try
            {
                await spExecutor.ExecuteStoredProcedureAsync("sp_UserRepoAssociation", parameters);
                return (true, 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Error executing UserRepoAssociation delete stored procedure");
                return (false, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing UserRepoAssociation delete stored procedure");
                throw;
            }
        }

        public async Task<(int Id, int totalCount)> EditUser(UserRepoAssociationUpdateRequest request)
        {
            var parameters = new[] {
                new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                new SqlParameter("@UserId", SqlDbType.Int) { Value = request.UserId },
                new SqlParameter("@RepoId", SqlDbType.Int) { Value = request.RepoId },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = request.IsActive },
                new SqlParameter("@UpdatedBy", SqlDbType.Int) { Value = request.UpdatedBy },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
            try
            {
                await spExecutor.ExecuteStoredProcedureAsync("sp_UserRepoAssociation", parameters);
                return (request.Id, 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Error executing UserRepoAssociation update stored procedure");
                return (-1, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing UserRepoAssociation update stored procedure");
                throw;
            }
        }
    }
}
