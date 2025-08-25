using JiJiBotApp_Backend.Data;
using JiJiBotApp_Backend.DTOs.Model.Role;
using JiJiBotApp_Backend.DTOs.SearchRequests.Role;
using JiJiBotApp_Backend.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;
using static JiJiBotApp_Backend.Repositories.Role.RoleRepository;

namespace JiJiBotApp_Backend.Repositories.Role
{
        public class RoleRepository(IStoredProcedureExecutor spExecutor, ILogger<RoleRepository> logger) : IRoleRepository
        {
            public async Task<(IEnumerable<RoleListModel> records, int totalCount)> SearchRole(RoleSearchRequest request)
            {
                var parameters = new[]
                {
                new SqlParameter("@StartIndex", SqlDbType.Int) { Value = request.StartIndex },
                new SqlParameter("@EndIndex", SqlDbType.Int) { Value = request.EndIndex },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

                try
                {
                    var result = await spExecutor.ExecuteStoredProcedureAsync("sp_Role", parameters);
                    var roleList = result.ToList<RoleListModel>();
                    int totalCount = roleList.Count;
                    return (roleList, totalCount);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing Role search stored procedure");
                    throw;
                }
            }

            public async Task<(int roleId, int totalCount)> AddRole(RoleAddRequest request)
            {
                var parameters = new[]
                {
                   new SqlParameter("@RoleName", SqlDbType.VarChar) { Value = request.RoleName },
                   new SqlParameter("@RoleCode", SqlDbType.VarChar) { Value = request.RoleCode },
                   new SqlParameter("@CompanyId", SqlDbType.Int) { Value = request.CompanyId },
                   new SqlParameter("@Description", SqlDbType.VarChar) { Value = request.Description ?? string.Empty },
                   new SqlParameter("@CreatedBy", SqlDbType.Int) { Value = request.CreatedBy },
                   new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
                };

                try
                {
                    var result = await spExecutor.ExecuteStoredProcedureAsync("sp_Role", parameters);
                return (Convert.ToInt32(result.Rows[0]["NewRoleId"]), 1);
                //return (Convert.ToInt32(result), 1);
                 }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "SQL error executing Role add stored procedure");
                    return (-1, -1);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing Role add stored procedure");
                    throw;
                }
            }

            public async Task<(bool result, int totalCount)> DeleteRole(RoleDeleteRequest request)
            {
                var parameters = new[]
                {
                new SqlParameter("@RoleId", SqlDbType.Int) { Value = request.RoleId },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

                try
                {
                    var result = await spExecutor.ExecuteStoredProcedureAsync("sp_Role", parameters);
                    return (true, 1);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "SQL error executing Role delete stored procedure");
                    return (false, -1);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing Role delete stored procedure");
                    throw;
                }
            }

            public async Task<(int roleId, int totalCount)> EditRole(RoleUpdateRequest request)
            {
                var parameters = new[]
                {
                new SqlParameter("@RoleId", SqlDbType.Int) { Value = request.RoleId },
                new SqlParameter("@RoleName", SqlDbType.VarChar) { Value = request.RoleName },
                new SqlParameter("@RoleCode", SqlDbType.VarChar) { Value = request.RoleCode },
                new SqlParameter("@CompanyId", SqlDbType.Int) { Value = request.CompanyId },
                new SqlParameter("@Description", SqlDbType.VarChar) { Value = request.Description ?? string.Empty },
                new SqlParameter("@UpdatedBy", SqlDbType.Int) { Value = request.UpdatedBy },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

                try
                {
                    var result = await spExecutor.ExecuteStoredProcedureAsync("sp_Role", parameters);
                    return (request.RoleId, 1);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "SQL error executing Role update stored procedure");
                    return (-1, -1);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing Role update stored procedure");
                    throw;
                }
            }
        }
    
}
