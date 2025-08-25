using JiJiBotApp_Backend.Data;
using JiJiBotApp_Backend.DTOs.Model.User;
using JiJiBotApp_Backend.DTOs.SearchRequests.User;
using JiJiBotApp_Backend.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace JiJiBotApp_Backend.Repositories.User
{
        public class UserRepository(IStoredProcedureExecutor spExecutor, ILogger<UserRepository> logger) : IUserRepository
        {
            public async Task<(IEnumerable<UserListModel> records, int totalCount)> SearchUser(UserSearchRequest request)
            {
                var parameters = new[] {
                new SqlParameter("@StartIndex", SqlDbType.Int) { Value = request.StartIndex },
                new SqlParameter("@EndIndex", SqlDbType.Int) { Value = request.EndIndex },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
                try
                {
                    var result = await spExecutor.ExecuteStoredProcedureAsync("Sp_UsersCrud", parameters);
                    var userList = result.ToList<UserListModel>();
                    int totalCount = userList.Count;
                    return (userList, totalCount);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing User search stored procedure");
                    throw;
                }
            }

            public async Task<(int userId, int totalCount)> AddUser(UserAddRequest request)
            {
                var parameters = new[] {
                new SqlParameter("@UserName", SqlDbType.VarChar) { Value = request.UserName },
                new SqlParameter("@UserEmail", SqlDbType.VarChar) { Value = request.UserEmail },
                new SqlParameter("@UserPassword", SqlDbType.VarChar) { Value = request.UserPassword },
                new SqlParameter("@RoleId", SqlDbType.VarChar) { Value = request.RoleId },
                new SqlParameter("@CreatedBy", SqlDbType.Int) { Value = request.CreatedBy },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
                try
                {
                    var result = await spExecutor.ExecuteStoredProcedureAsync("Sp_UsersCrud", parameters);
                    return (Convert.ToInt32(result.Rows[0]["NewUserId"]), 1);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "Error executing User add stored procedure");
                    return (-1, -1);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing User add stored procedure");
                    throw;
                }
            }

            public async Task<(bool result, int totalCount)> DeleteUser(UserDeleteRequest request)
            {
                var parameters = new[] {
                new SqlParameter("@UserId", SqlDbType.Int) { Value = request.UserId },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
                try
                {
                    var result = await spExecutor.ExecuteStoredProcedureAsync("Sp_UsersCrud", parameters);
                    return (true, 1);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "Error executing User delete stored procedure");
                    return (false, -1);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing User delete stored procedure");
                    throw;
                }
            }

            public async Task<(int userId, int totalCount)> EditUser(UserUpdateRequest request)
            {
                var parameters = new[] {
                new SqlParameter("@UserId", SqlDbType.Int) { Value = request.UserId },
                new SqlParameter("@UserName", SqlDbType.VarChar) { Value = request.UserName },
                new SqlParameter("@UserEmail", SqlDbType.VarChar) { Value = request.UserEmail },
                new SqlParameter("@UserPassword", SqlDbType.VarChar) { Value = request.UserPassword },
                new SqlParameter("@RoleId", SqlDbType.VarChar) { Value = request.RoleId },
                new SqlParameter("@UpdatedBy", SqlDbType.Int) { Value = request.UpdatedBy },
                 new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
                try
                {
                    var result = await spExecutor.ExecuteStoredProcedureAsync("Sp_UsersCrud", parameters);
                    return (request.UserId, 1);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "Error executing User update stored procedure");
                    return (-1, -1);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing User update stored procedure");
                    throw;
                }
            }
        }
}