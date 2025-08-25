using JiJiBotApp_Backend.Controllers.BaseApi;
using JiJiBotApp_Backend.DTOs.Model.Response;
using JiJiBotApp_Backend.DTOs.Model.Screen;
using JiJiBotApp_Backend.DTOs.SearchRequests.Screen;
using JiJiBotApp_Backend.Services.Screen;
using Microsoft.AspNetCore.Mvc;

namespace JiJiBotApp_Backend.Controllers.Screen
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenController(IScreenService ScreenService, ILogger<ScreenController> logger) : BaseApiController
    {
        [HttpPost("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ScreenListModel>>>> SearchScreen([FromBody] SearchScreenRequest request)
        {
            try
            {
                var (records, totalCount) = await ScreenService.SearchScreen(request);
                return HandleSuccess(records, "Screen List retrieved successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing dropdown request");
                return HandleError<IEnumerable<ScreenListModel>>(ex, "Failed to retrieve Screen List");
            }
        }
        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<List<ScreenReturnModel>>>> AddScreen([FromBody] AddScreenRequest request)
        {
            try
            {
                var (id, totalCount) = await ScreenService.AddScreen(request);
                List<ScreenReturnModel> model = new List<ScreenReturnModel>
                {
                    new()
                    {
                        Id = id,
                        TotalCount = totalCount
                    }
                };
                if (id == -1)
                {
                    return HandleSuccess(model, "Screen name already exists", totalCount);
                }
                else
                {
                    return HandleSuccess(model, "Screen added successfully", totalCount);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing add request");
                return HandleError<List<ScreenReturnModel>>(ex, "Failed to add Screen");
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteScreen([FromBody] DeleteScreenRequest request)
        {
            try
            {
                var (isDeleted, totalCount) = await ScreenService.DeleteScreen(request);

                if (!isDeleted)
                    return HandleSuccess(false, "Failed to delete Screen");

                return HandleSuccess(isDeleted, "Screen deleted successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing delete request");
                return HandleError<bool>(ex, "Failed to delete Screen");
            }
        }

        [HttpPost("edit")]
        public async Task<ActionResult<ApiResponse<List<ScreenReturnModel>>>> EditScreen([FromBody] UpdateScreenRequest request)
        {
            try
            {
                var (Id, totalCount) = await ScreenService.EditScreen(request);

                List<ScreenReturnModel> model = new()
                {
                    new()
                    {
                         Id = Id,
                        TotalCount = totalCount
                    }
                };

                if (Id == -1)
                {
                    return HandleSuccess(model, "Screen name already exists", totalCount);
                }
                else
                {
                    return HandleSuccess(model, "Screen updated successfully", totalCount);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing edit request");
                return HandleError<List<ScreenReturnModel>>(ex, "Failed to update Screen");
            }
        }

    }
}
