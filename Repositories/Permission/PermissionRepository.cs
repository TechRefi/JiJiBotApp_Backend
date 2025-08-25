using JiJiBotApp_Backend.Data;
using JiJiBotApp_Backend.DTOs.Model.Permission;
using JiJiBotApp_Backend.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;
using static JiJiBotApp_Backend.DTOs.SearchRequests.Permission.PermissionRequest;

namespace JiJiBotApp_Backend.Repositories.Permission
{

    public class PermissionRepository(IStoredProcedureExecutor spExecutor, ILogger<PermissionRepository> logger) : IPermissionRepository
    {
        public async Task<(IEnumerable<PermissionListModel> records, int totalCount)> SearchPermission(SearchPermissionRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@StartIndex", SqlDbType.Int) { Value = request.StartIndex },
                new SqlParameter("@EndIndex", SqlDbType.Int) { Value = request.EndIndex },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

            try
            {
                var result = await spExecutor.ExecuteStoredProcedureAsync("sp_Permissions", parameters);
                var permissionList = result.ToList<PermissionListModel>();
                int totalCount = permissionList.Count;
                return (permissionList, totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Permission search stored procedure");
                throw;
            }
        }

        public async Task<(int permissionId, int totalCount)> AddPermission(AddPermissionRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@permission_name", SqlDbType.VarChar) { Value = request.Permission_name },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = request.IsActive },
                new SqlParameter("@created_by", SqlDbType.Int) { Value = request.created_by },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

            try
            {
                var result = await spExecutor.ExecuteStoredProcedureAsync("sp_Permissions", parameters);
                return (Convert.ToInt32(result.Rows[0]["NewPermissionId"]), 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL error executing Permission add stored procedure");
                return (-1, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Permission add stored procedure");
                throw;
            }
        }

        public async Task<(bool result, int totalCount)> DeletePermission(DeletePermissionRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

            try
            {
                await spExecutor.ExecuteStoredProcedureAsync("sp_Permissions", parameters);
                return (true, 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL error executing Permission delete stored procedure");
                return (false, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Permission delete stored procedure");
                throw;
            }
        }

        public async Task<(int permissionId, int totalCount)> EditPermission(UpdatePermissionRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                new SqlParameter("@Permission_name", SqlDbType.VarChar) { Value = request.Permission_name },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = request.IsActive },
                new SqlParameter("@updated_by", SqlDbType.Int) { Value = request.updated_by },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

            try
            {
                await spExecutor.ExecuteStoredProcedureAsync("sp_Permissions", parameters);
                return (request.Id, 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL error executing Permission update stored procedure");
                return (-1, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Permission update stored procedure");
                throw;
            }
        }
    }

}
