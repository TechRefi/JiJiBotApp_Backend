using Microsoft.Data.SqlClient;
using JiJiBotApp_Backend.Data;
using JiJiBotApp_Backend.Extensions;
using JiJiBotApp_Backend.Utils;
using System.Data;
using JiJiBotApp_Backend.DTOs.SearchRequests.Common;
using JiJiBotApp_Backend.DTOs.Model.Common;

namespace JiJiBotApp_Backend.Repositories.Common
{
    public class CommonRepository(IStoredProcedureExecutor spExecutor, ILogger<CommonRepository> logger) : ICommonRepository
    {
        public async Task<List<DropdownItem>> GetDropdownItemsAsync(DropdownRequest request)
        {
            try
            {
                //GetProcedureName(request.DropdownType)
                var procedureName = "uspSelectIdName" + request.DropdownType;



                var parameters = request.Parameters.Select(kvp =>
                {
                    var value = CommonUtils.ConvertJsonElementIfNeeded(kvp.Value);

                    var sqlType = CommonUtils.ParameterTypeMap.TryGetValue(kvp.Key, out var mappedType)
                        ? mappedType
                        : SqlDbType.NVarChar;

                    var param = new SqlParameter("@" + kvp.Key, sqlType)
                    {
                        Value = value ?? DBNull.Value
                    };

                    return param;
                }).ToArray();

                var result = await spExecutor.ExecuteStoredProcedureAsync(procedureName, parameters);

                var dropdownItems = result.ToList<DropdownItem>();
                return dropdownItems;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error fetching dropdown items for type: {DropdownType}", request.DropdownType);
                throw;
            }
        }
    }
}
