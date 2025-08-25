using JiJiBotApp_Backend.Data;
using JiJiBotApp_Backend.DTOs.Model.UserRoleAssociation;
using JiJiBotApp_Backend.DTOs.SearchRequests.UserRoleAssociation;
using JiJiBotApp_Backend.Extensions;
using JiJiBotApp_Backend.Services.UserRoleAssociation;
using Microsoft.Data.SqlClient;
using System.Data;

namespace JiJiBotApp_Backend.Repositories.UserRoleAssociation
{
    public class UserRoleAssociationRepository(IStoredProcedureExecutor spExecutor, ILogger<UserRoleAssociationRepository> logger) :
        IUserRoleAssociationRepository
    {
        public async Task<(IEnumerable<UserRoleAssociationListModel> records, int totalCount)> SearchUserRoleAssociation(UserRoleAssociationSearchRequest request)
        {
            var parameters = new[] {
                new SqlParameter("@StartIndex", SqlDbType.Int) { Value = request.StartIndex },
                new SqlParameter("@EndIndex", SqlDbType.Int) { Value = request.EndIndex },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
            try
            {
                var result = await spExecutor.ExecuteStoredProcedureAsync("Sp_UserRoleAssociationCrud", parameters);
                var list = result.ToList<UserRoleAssociationListModel>();
                int totalCount = list.Count;
                return (list, totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing UserRoleAssociation search stored procedure");
                throw;
            }
        }

        public async Task<(int id, int totalCount)> AddUserRoleAssociation(UserRoleAssociationAddRequest request)
        {
            var parameters = new[] {
                new SqlParameter("@UserId", SqlDbType.Int) { Value = request.UserId },
                new SqlParameter("@RoleId", SqlDbType.Int) { Value = request.RoleId },
                new SqlParameter("@CreatedBy", SqlDbType.Int) { Value = request.CreatedBy },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = request.IsActive },
                new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
            try
            {
                var result = await spExecutor.ExecuteStoredProcedureAsync("Sp_UserRoleAssociationCrud", parameters);
                return (Convert.ToInt32(result.Rows[0]["NewId"]), 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Error executing UserRoleAssociation add stored procedure");
                return (-1, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing UserRoleAssociation add stored procedure");
                throw;
            }
        }

        public async Task<(bool result, int totalCount)> DeleteUserRoleAssociation(UserRoleAssociationDeleteRequest request)
        {
            var parameters = new[] {
                new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
            try
            {
                await spExecutor.ExecuteStoredProcedureAsync("Sp_UserRoleAssociationCrud", parameters);
                return (true, 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Error executing UserRoleAssociation delete stored procedure");
                return (false, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing UserRoleAssociation delete stored procedure");
                throw;
            }
        }

        public async Task<(int id, int totalCount)> EditUserRoleAssociation(UserRoleAssociationUpdateRequest request)
        {
            var parameters = new[] {
                new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                new SqlParameter("@UserId", SqlDbType.Int) { Value = request.UserId },
                new SqlParameter("@RoleId", SqlDbType.Int) { Value = request.RoleId },
                new SqlParameter("@UpdatedBy", SqlDbType.Int) { Value = request.UpdatedBy },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = request.IsActive },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
            try
            {
                await spExecutor.ExecuteStoredProcedureAsync("Sp_UserRoleAssociationCrud", parameters);
                return (request.Id, 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Error executing UserRoleAssociation update stored procedure");
                return (-1, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing UserRoleAssociation update stored procedure");
                throw;
            }
        }
    }
}
