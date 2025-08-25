using JiJiBotApp_Backend.Data;
using JiJiBotApp_Backend.DTOs.Model.Screen;
using JiJiBotApp_Backend.DTOs.SearchRequests.Screen;
using JiJiBotApp_Backend.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;
using static JiJiBotApp_Backend.DTOs.SearchRequests.Repositories.RepositoryRequest;
namespace JiJiBotApp_Backend.Repositories.Screen
{
    public class ScreenRepository(IStoredProcedureExecutor spExecutor, ILogger<ScreenRepository> logger) : IScreenRepository
    {
        public async Task<(IEnumerable<ScreenListModel> records, int totalCount)> SearchScreen(SearchScreenRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@StartIndex", SqlDbType.Int) { Value = request.StartIndex },
                new SqlParameter("@EndIndex", SqlDbType.Int) { Value = request.EndIndex },
                new SqlParameter("@CompanyId", SqlDbType.Int) { Value = request.CompanyId},
                //new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

            try
            {
                var result = await spExecutor.ExecuteStoredProcedureAsync("Sp_Screen", parameters);
                var screenList = result.ToList<ScreenListModel>();
                int totalCount = screenList.Count;
                return (screenList, totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Screens search stored procedure");
                throw;
            }
        }
        public async Task<(int Id, int totalCount)> AddScreen(AddScreenRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@ScreenName", SqlDbType.VarChar, 150) { Value = request.ScreenName },
                new SqlParameter("@ScreenCode", SqlDbType.VarChar, 50) { Value = request.ScreenCode },
                new SqlParameter("@Description", SqlDbType.VarChar, 250) { Value = request.Description },
                new SqlParameter("@ParentId", SqlDbType.Int) { Value = request.Parent_Id },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = request.IsActive },
                new SqlParameter("@Created_By", SqlDbType.Int) { Value = request.CreatedBy },
                  new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

            try
            {
                var result = await spExecutor.ExecuteScalarStoredProcedureAsync("Sp_ScreenCrud", parameters);
                //return (Convert.ToInt32(result.Rows[0]["NewScreenId"]), 1);
                return (Convert.ToInt32(result), 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL error executing Screen add stored procedure");
                return (-1, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Screen add stored procedure");
                throw;
            }
        }
        public async Task<(int Id, int totalCount)> EditScreen(UpdateScreenRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                new SqlParameter("@ScreenName", SqlDbType.VarChar, 150) { Value = request.ScreenName },
                new SqlParameter("@ScreenCode", SqlDbType.VarChar, 50) { Value = request.ScreenCode },
                new SqlParameter("@Description", SqlDbType.VarChar, 250) { Value = request.Description },
                new SqlParameter("@ParentId", SqlDbType.Int) { Value = request.Parent_Id },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = request.IsActive },
                new SqlParameter("@Updated_By", SqlDbType.Int) { Value = request.UpdatedBy },
                new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

            try
            {
                await spExecutor.ExecuteStoredProcedureAsync("Sp_ScreenCrud", parameters);
                return (request.Id, 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL error executing Screen update stored procedure");
                return (-1, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Screen update stored procedure");
                throw;
            }
        }
        public async Task<(bool result, int totalCount)> DeleteScreen(DeleteScreenRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

            try
            {
                await spExecutor.ExecuteStoredProcedureAsync("Sp_ScreenCrud", parameters);
                return (true, 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL error executing Screen delete stored procedure");
                return (false, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Screen delete stored procedure");
                throw;
            }
        }
    }
}
