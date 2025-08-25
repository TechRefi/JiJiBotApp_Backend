using JiJiBotApp_Backend.Data;
using JiJiBotApp_Backend.DTOs.Model.Role;
using JiJiBotApp_Backend.DTOs.Model.RolePermissionAssociation;
using JiJiBotApp_Backend.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;
using static JiJiBotApp_Backend.DTOs.SearchRequests.RolePermissionAssociation.RolePermissionAssociationRequest;

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

        // New methods for role-centric permission management
        public async Task<(List<RolePermissionModel> permissions, int totalCount)> GetRolePermissions(int roleId)
        {
            var parameters = new[]
            {
                    new SqlParameter("@RoleId", SqlDbType.Int) { Value = roleId },
                    new SqlParameter("@Action", SqlDbType.VarChar) { Value = "GetRolePermissions" }
                };

            try
            {
                var result = await spExecutor.ExecuteStoredProcedureAsync("sp_RolePermissionAssociation", parameters);
                var permissions = new List<RolePermissionModel>();

                foreach (DataRow row in result.Rows)
                {
                    permissions.Add(new RolePermissionModel
                    {
                        PermissionId = Convert.ToInt32(row["PermissionId"]),
                        PermissionName = row["PermissionName"].ToString() ?? string.Empty,
                        IsGranted = Convert.ToBoolean(row["IsGranted"])
                    });
                }

                return (permissions, permissions.Count);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing GetRolePermissions stored procedure for roleId: {roleId}", roleId);
                throw;
            }
        }

        public async Task<(IEnumerable<RolePermissionModel> availablePermissions, int totalCount)> GetAvailablePermissions()
        {
            var parameters = new[]
            {
                    new SqlParameter("@Action", SqlDbType.VarChar) { Value = "GetAvailablePermissions" }
                };

            try
            {
                var result = await spExecutor.ExecuteStoredProcedureAsync("sp_RolePermissionAssociation", parameters);
                var permissions = new List<RolePermissionModel>();

                foreach (DataRow row in result.Rows)
                {
                    permissions.Add(new RolePermissionModel
                    {
                        PermissionId = Convert.ToInt32(row["PermissionId"]),
                        PermissionName = row["PermissionName"].ToString() ?? string.Empty,
                        IsGranted = false // Default to false for available permissions
                    });
                }

                return (permissions, permissions.Count);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing GetAvailablePermissions stored procedure");
                throw;
            }
        }

        public async Task<bool> DeleteRolePermissionsByRole(int roleId)
        {
            var parameters = new[]
            {
                    new SqlParameter("@RoleId", SqlDbType.Int) { Value = roleId },
                    new SqlParameter("@Action", SqlDbType.VarChar) { Value = "DeleteByRole" }
                };

            try
            {
                await spExecutor.ExecuteNonQueryStoredProcedureAsync("sp_RolePermissionAssociation", parameters);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing DeleteRolePermissionsByRole stored procedure for roleId: {roleId}", roleId);
                return false;
            }
        }

        public async Task<bool> AddRolePermissions(int roleId, List<int> permissionIds, int enteredBy)
        {
            try
            {
                foreach (var permissionId in permissionIds)
                {
                    var parameters = new[]
                    {
                            new SqlParameter("@RoleId", SqlDbType.Int) { Value = roleId },
                            new SqlParameter("@PermissionId", SqlDbType.Int) { Value = permissionId },
                            new SqlParameter("@CreatedBy", SqlDbType.Int) { Value = enteredBy },
                            new SqlParameter("@IsActive", SqlDbType.Bit) { Value = true },
                            new SqlParameter("@Action", SqlDbType.VarChar) { Value = "Insert" }
                        };

                    await spExecutor.ExecuteNonQueryStoredProcedureAsync("sp_RolePermissionAssociation", parameters);
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing AddRolePermissions stored procedure for roleId: {roleId}", roleId);
                return false;
            }
        }
    }

}
