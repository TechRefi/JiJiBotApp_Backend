using Microsoft.Data.SqlClient;
using System.Data;
using JiJiBotApp_Backend.Data;
using JiJiBotApp_Backend.DTOs.Model.Common;
using JiJiBotApp_Backend.DTOs.Model.Company;
using JiJiBotApp_Backend.DTOs.SearchRequests.Company;
using JiJiBotApp_Backend.Extensions;

namespace JiJiBotApp_Backend.Repositories.Company
{
    public class CompanyRepository(IStoredProcedureExecutor spExecutor, ILogger<CompanyRepository> logger) : ICompanyRepository
    {
        public async Task<(IEnumerable<CompanyListModel> records, int totalCount)> SearchCompany(SearchCompanyRequest request)
        {
            var parameters = new[] {
                new SqlParameter("@StartIndex", SqlDbType.Int) { Value = request.StartIndex },
                new SqlParameter("@EndIndex", SqlDbType.Int) { Value = request.EndIndex },
                new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
            try
            {
                var result = await spExecutor.ExecuteStoredProcedureAsync("sp_Company", parameters);
                var companyList = result.ToList<CompanyListModel>();
                int totalCount = companyList.Count;
                return (companyList, totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Company search stored procedure");
                throw;
            }
        }
        public async Task<(int companyId, int totalCount)> AddCompany(AddCompanyRequest request)
        {
            var parameters = new[] {
                new SqlParameter("@CompanyName", SqlDbType.VarChar) { Value = request.CompanyName },
                new SqlParameter("@CompanyCode", SqlDbType.VarChar) { Value = request.CompanyCode },
                new SqlParameter("@Description", SqlDbType.VarChar) { Value = request.Description ?? string.Empty },
                new SqlParameter("@isActive", SqlDbType.Bit) { Value = request.isActive },
                new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
            try
            {
                var result = await spExecutor.ExecuteStoredProcedureAsync("sp_Company", parameters);
                return (Convert.ToInt32(result.Rows[0]["NewCompanyID"]), 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Error executing Company add stored procedure");
                return (-1, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Company add stored procedure");
                throw;
            }
        }
        public async Task<(bool result, int totalCount)> DeleteCompany(DeleteCompanyRequest request)
        {
            var parameters = new[] {
                new SqlParameter("@CompanyId", SqlDbType.Int) { Value = request.CompanyId },
                new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
            try
            {
                var result = await spExecutor.ExecuteStoredProcedureAsync("sp_Company", parameters);
                return (true, 1);
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "Error executing Company delete stored procedure");
                return (false, -1);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing Company delete stored procedure");
                throw;
            }
        }
        public async Task<(int companyId, int totalCount)> EditCompany(UpdateCompanyRequest request)
        {
            {
                var parameters = new[] {
                new SqlParameter("@CompanyId", SqlDbType.Int) { Value = request.CompanyId },
                new SqlParameter("@CompanyName", SqlDbType.VarChar) { Value = request.CompanyName },
                new SqlParameter("@CompanyCode", SqlDbType.VarChar) { Value = request.CompanyCode },
                new SqlParameter("@Description", SqlDbType.VarChar) { Value = request.Description ?? string.Empty },
                new SqlParameter("@isActive", SqlDbType.Bit) { Value = request.isActive },
                new SqlParameter("@Action", SqlDbType.VarChar) { Value = request.Action }
            };
                try
                {
                    var result = await spExecutor.ExecuteStoredProcedureAsync("sp_Company", parameters);
                    return (request.CompanyId, 1);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "Error executing Company update stored procedure");
                    return (-1, -1);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing Company update stored procedure");
                    throw;
                }
            }
        }
    }
}