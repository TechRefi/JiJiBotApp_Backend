using JiJiBotApp_Backend.Data;
using JiJiBotApp_Backend.DTOs.Model.Screen;
using JiJiBotApp_Backend.DTOs.SearchRequests.ScreenAssociation;
using JiJiBotApp_Backend.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace JiJiBotApp_Backend.Repositories.ScreenAssociation
{
    public class ScreenAssociationRepository(
        IStoredProcedureExecutor spExecutor,
        ILogger<ScreenAssociationRepository> logger) : IScreenAssociationRepository
    {
        public async Task<(IEnumerable<ScreenListModel> records, int totalCount)> SearchScreenAssociation(ScreenAssociationSearchRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@StartIndex", SqlDbType.Int) { Value = request.StartIndex },
                new SqlParameter("@EndIndex", SqlDbType.Int) { Value = request.EndIndex },
                 new SqlParameter("@CompanyId", SqlDbType.Int) { Value = request.CompanyId},

            };

            try
            {
                var result = await spExecutor.ExecuteStoredProcedureAsync("SP_ScreenAssociation", parameters);
                var list = result.ToList<ScreenListModel>();
                int totalCount = list.Count;
                return (list, totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing ScreenAssociation search stored procedure");
                throw;
            }
        }
        public async Task<(int Id, int totalCount)> AddScreenAssociation(ScreenAssociationAddRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@Screen_Id", SqlDbType.Int) { Value = request.ScreenId },
                new SqlParameter("@Company_Id", SqlDbType.Int) { Value = request.CompanyId },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = request.IsActive },
                new SqlParameter("@Created_By", SqlDbType.Int) { Value = request.CreatedBy },
                  new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
            try
            {
                var result = await spExecutor.ExecuteScalarStoredProcedureAsync("SP_ScreenAssociation", parameters);
                return (Convert.ToInt32(result), 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL error executing ScreenAssociation add stored procedure");
                return (-1, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing ScreenAssociation add stored procedure");
                throw;
            }
        }
        public async Task<(bool result, int totalCount)> DeleteScreenAssociation(ScreenAssociationDeleteRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                  new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
            try
            {
                var rowsAffected = await spExecutor.ExecuteNonQueryStoredProcedureAsync("SP_ScreenAssociation", parameters);
                return (true, 1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing ScreenAssociation delete stored procedure");
                throw;
            }
        }
        public async Task<(int Id, int totalCount)> EditScreenAssociation(ScreenAssociationUpdateRequest request)
        {
            var parameters = new[]
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                new SqlParameter("@Screen_Id", SqlDbType.Int) { Value = request.ScreenId },
                new SqlParameter("@Company_Id", SqlDbType.Int) { Value = request.CompanyId },
                new SqlParameter("@IsActive", SqlDbType.Bit) { Value = request.IsActive },
                new SqlParameter("@Updated_By", SqlDbType.Int) { Value = request.UpdatedBy },
                new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };

            try
            {
                await spExecutor.ExecuteStoredProcedureAsync("SP_ScreenAssociation", parameters);
                return (request.Id, 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "SQL error executing ScreenAssociation update stored procedure");
                return (-1, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing ScreenAssociation update stored procedure");
                throw;
            }
        }

    }

}
