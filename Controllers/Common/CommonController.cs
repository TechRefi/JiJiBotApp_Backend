using JiJiBotApp_Backend.Controllers.BaseApi;
using JiJiBotApp_Backend.DTOs.Model.Common;
using JiJiBotApp_Backend.DTOs.Model.Response;
using JiJiBotApp_Backend.DTOs.SearchRequests.Common;
using JiJiBotApp_Backend.Services.Common;
using Microsoft.AspNetCore.Mvc;


namespace JiJiBotApp_Backend.Controllers.Common
{
    public class CommonController(ICommonService commonService, ILogger<CommonController> logger) : BaseApiController
    {
        [HttpPost("dropdown")]
        public async Task<ActionResult<ApiResponse<List<DropdownItem>>>> GetDropdownItems([FromBody] DropdownRequest request)
        {
            try
            {
                var result = await commonService.GetDropdownItemsAsync(request);
                return HandleSuccess(result, "Dropdown data fetched successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing dropdown request");
                return HandleError<List<DropdownItem>>(ex, "An error occurred while fetching dropdown data");
            }
        }
    }
}
