using System.Data;
using System.Text.Json;

namespace JiJiBotApp_Backend.Utils
{
    public class CommonUtils
    {
        public static object? ConvertJsonElementIfNeeded(object? value)
        {
            if (value is not JsonElement jsonElement)
                return value;

            return jsonElement.ValueKind switch
            {
                JsonValueKind.String => jsonElement.GetString(),
                JsonValueKind.Number => jsonElement.TryGetInt32(out var i) ? i :
                                        jsonElement.TryGetDecimal(out var d) ? d :
                                        jsonElement.GetDouble(), // fallback
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                JsonValueKind.Null => null,
                _ => jsonElement.ToString()
            };
        }

        public static readonly Dictionary<string, SqlDbType> ParameterTypeMap = new()
        {
            { "CompanyId", SqlDbType.Int },
            { "POId", SqlDbType.Int },
            { "GroupId", SqlDbType.Int },
            { "ShopId", SqlDbType.Int },
            { "CategoryId", SqlDbType.Int },
            { "LineItemId", SqlDbType.Int },
            { "Type", SqlDbType.NVarChar },
            { "GroupType", SqlDbType.Int },
            { "SupplierId", SqlDbType.Int },
        };
    }
}
