using JiJiBotApp_Backend.DTOs.Model.Common;
using JiJiBotApp_Backend.DTOs.SearchRequests.Common;
using JiJiBotApp_Backend.Repositories.Common;

namespace JiJiBotApp_Backend.Services.Common
{
    public class CommonService(ICommonRepository repository, ILogger<CommonService> logger) : ICommonService
    {
        public async Task<List<DropdownItem>> GetDropdownItemsAsync(DropdownRequest request)
        {
            logger.LogInformation("Fetching dropdown items for type: {DropdownType}", request.DropdownType);
            return await repository.GetDropdownItemsAsync(request);
        }
        public async Task<bool> ValidateDropdownRequestAsync(DropdownRequest request)
        {
            if (string.IsNullOrEmpty(request.DropdownType) || request.Parameters == null || !request.Parameters.Any())
                return false;

            return request.DropdownType.ToLower() switch
            {
                "getshopbygrouptype" => request.Parameters.ContainsKey("CompanyId") && request.Parameters.ContainsKey("ShopId") && request.Parameters.ContainsKey("GroupType"),
                "supplier" => request.Parameters.ContainsKey("CompanyId"),
                "lineitemassortment" => request.Parameters.ContainsKey("CompanyId"),
                "productattribute4" => request.Parameters.ContainsKey("CompanyId"),
                "productattribute2" => request.Parameters.ContainsKey("CompanyId"),
                "productattribute5" => request.Parameters.ContainsKey("CompanyId"),
                "productattribute6" => request.Parameters.ContainsKey("CompanyId"),
                "productattribute7" => request.Parameters.ContainsKey("CompanyId"),
                "productattribute8" => request.Parameters.ContainsKey("CompanyId"),
                "productattribute9" => request.Parameters.ContainsKey("CompanyId"),
                "purchaseorder" => request.Parameters.ContainsKey("CompanyId") && request.Parameters.ContainsKey("ShopId") && request.Parameters.ContainsKey("SupplierId"),
                "taxcode" => request.Parameters.ContainsKey("CompanyId"),
                "shop" => request.Parameters.ContainsKey("CompanyId") && request.Parameters.ContainsKey("Type"),
                "productattribute1lineitem" => request.Parameters.ContainsKey("CompanyId") && request.Parameters.ContainsKey("LineItemId"),
                "productcombinationlineitem" => request.Parameters.ContainsKey("CompanyId") && request.Parameters.ContainsKey("LineItemId"),
                "productsizelineitemcode" => request.Parameters.ContainsKey("CompanyId") && request.Parameters.ContainsKey("LineItemId"),
                "productsizelineitem" => request.Parameters.ContainsKey("CompanyId") && request.Parameters.ContainsKey("LineItemId"),
                "productattribute3lineitem" => request.Parameters.ContainsKey("CompanyId") && request.Parameters.ContainsKey("LineItemId"),
                "productgrouplineitem" => request.Parameters.ContainsKey("CompanyId") && request.Parameters.ContainsKey("LineItemId"),
                "categorylineitem" => request.Parameters.ContainsKey("CompanyId") && request.Parameters.ContainsKey("LineItemId"),
                "lineitem" => request.Parameters.ContainsKey("CompanyId") && request.Parameters.ContainsKey("GroupId"),
                "subcategorycategory" => request.Parameters.ContainsKey("CompanyId") && request.Parameters.ContainsKey("CategoryId"),
                //
                 

                _ => false
            };
        }
    }

}
