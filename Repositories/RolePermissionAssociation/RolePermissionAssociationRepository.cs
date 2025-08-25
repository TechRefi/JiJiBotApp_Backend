using JiJiBotApp_Backend.Data;
using JiJiBotApp_Backend.DTOs.Model.RolePermissionAssociation;
using JiJiBotApp_Backend.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;
using static JiJiBotApp_Backend.DTOs.SearchRequests.RolePermissionAssociation.RolePermissionAssociationRequest;
using static JiJiBotApp_Backend.Repositories.RolePermissionAssociation.RolePermissionAssociationRepository;

namespace JiJiBotApp_Backend.Repositories.RolePermissionAssociation
{
    
        public class RolePermissionAssociationRepository(
      IStoredProcedureExecutor spExecutor,
      ILogger<RolePermissionAssociationRepository> logger) : IRolePermissionAssociationRepository
        {
            public async Task<(IEnumerable<RolePermissionAssociationListModel> records, int totalCount)> SearchRolePermissionAssociation(RolePermissionAssociationSearchRequest request)
            {
                var parameters = new[]
                {
                new SqlParameter("@StartIndex", SqlDbType.Int) { Value = request.StartIndex },
                new SqlParameter("@EndIndex", SqlDbType.Int) { Value = request.EndIndex },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }

            };

                try
                {
                    var result = await spExecutor.ExecuteStoredProcedureAsync("sp_RolePermissionAssociation", parameters);
                    var list = result.ToList<RolePermissionAssociationListModel>();
                    int totalCount = list.Count;
                    return (list, totalCount);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing RolePermissionAssociation search stored procedure");
                    throw;
                }
            }

            public async Task<(int id, int totalCount)> AddRolePermissionAssociation(RolePermissionAssociationAddRequest request)
            {
                var parameters = new[]
                {
                new SqlParameter("@RoleId", SqlDbType.Int) { Value = request.RoleId },
                new SqlParameter("@PermissionId", SqlDbType.Int) { Value = request.PermissionId },
                new SqlParameter("@CreatedBy", SqlDbType.Int) { Value = request.CreatedBy },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = request.IsActive },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

                try
                {
                    var result = await spExecutor.ExecuteStoredProcedureAsync("sp_RolePermissionAssociation", parameters);
                    return (Convert.ToInt32(result.Rows[0]["NewRolePermissionAssociationId"]), 1);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "SQL error executing RolePermissionAssociation add stored procedure");
                    return (-1, -1);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing RolePermissionAssociation add stored procedure");
                    throw;
                }
            }

            public async Task<(int id, int totalCount)> EditRolePermissionAssociation(RolePermissionAssociationUpdateRequest request)
            {
                var parameters = new[]
                {
                new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                new SqlParameter("@RoleId", SqlDbType.Int) { Value = request.RoleId },
                new SqlParameter("@PermissionId", SqlDbType.Int) { Value = request.PermissionId },
                new SqlParameter("@UpdatedBy", SqlDbType.Int) { Value = request.UpdatedBy },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = request.IsActive },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

                try
                {
                    var result = await spExecutor.ExecuteStoredProcedureAsync("sp_RolePermissionAssociation", parameters);
                    return (request.Id, 1);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "SQL error executing RolePermissionAssociation update stored procedure");
                    return (-1, -1);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing RolePermissionAssociation update stored procedure");
                    throw;
                }
            }

            public async Task<(bool result, int totalCount)> DeleteRolePermissionAssociation(RolePermissionAssociationDeleteRequest request)
            {
                var parameters = new[]
                {
                new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

                try
                {
                    var result = await spExecutor.ExecuteStoredProcedureAsync("sp_RolePermissionAssociation", parameters);
                    return (true, 1);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "SQL error executing RolePermissionAssociation delete stored procedure");
                    return (false, -1);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing RolePermissionAssociation delete stored procedure");
                    throw;
                }
            }
        }
   
}
