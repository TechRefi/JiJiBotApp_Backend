using JiJiBotApp_Backend.Controllers.BaseApi;
using JiJiBotApp_Backend.DTOs.Model.Response;
using JiJiBotApp_Backend.DTOs.Model.Screen;
using JiJiBotApp_Backend.DTOs.Model.ScreenAssociation;
using JiJiBotApp_Backend.DTOs.SearchRequests.ScreenAssociation;
using JiJiBotApp_Backend.Services.ScreenAssociation;
using Microsoft.AspNetCore.Mvc;

namespace TechRiFi.Controllers.ScreenAssociation
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenAssociationController(IScreenAssociationService screenAssociationService, ILogger<ScreenAssociationController> logger) : BaseApiController
    {
        [HttpPost("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ScreenListModel>>>> Search([FromBody] ScreenAssociationSearchRequest request)
        {
            try
            {
                var (records, totalCount) = await screenAssociationService.SearchScreenAssociation(request);
                return HandleSuccess(records, "Screen Association List retrieved successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing ScreenAssociation search request");
                return HandleError<IEnumerable<ScreenListModel>>(ex, "Failed to retrieve Screen Association List");
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<List<ScreenAssociationReturnModel>>>> AddScreenAssociation([FromBody] ScreenAssociationAddRequest request)
        {
            try
            {
                var (id, totalCount) = await screenAssociationService.AddScreenAssociation(request);

                List<ScreenAssociationReturnModel> model = new List<ScreenAssociationReturnModel>
                {
                    new()
                    {
                        Id = id,
                        TotalCount = totalCount
                    }
                };

                if (id == -1)
                {
                    return HandleSuccess(model, "Screen Association Already Exists", totalCount);
                }

                return HandleSuccess(model, "Screen Association added successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing ScreenAssociation add request");
                return HandleError<List<ScreenAssociationReturnModel>>(ex, "Failed to add Screen Association");
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteScreenAssociation([FromBody] ScreenAssociationDeleteRequest request)
        {
            try
            {
                var (isDeleted, totalCount) = await screenAssociationService.DeleteScreenAssociation(request);

                if (!isDeleted)
                    return HandleSuccess(false, "Failed to delete Screen Association");

                return HandleSuccess(isDeleted, "Screen Association deleted successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing ScreenAssociation delete request");
                return HandleError<bool>(ex, "Failed to delete Screen Association");
            }
        }

        [HttpPost("edit")]
        public async Task<ActionResult<ApiResponse<List<ScreenAssociationReturnModel>>>> EditScreenAssociation([FromBody] ScreenAssociationUpdateRequest request)
        {
            try
            {
                var (id, totalCount) = await screenAssociationService.EditScreenAssociation(request);

                List<ScreenAssociationReturnModel> model = new List<ScreenAssociationReturnModel>
                {
                    new()
                    {
                        Id = id,
                        TotalCount = totalCount
                    }
                };

                if (id == -1)
                {
                    return HandleSuccess(model, "Screen Association Already Exists", totalCount);
                }

                return HandleSuccess(model, "Screen Association updated successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing ScreenAssociation update request");
                return HandleError<List<ScreenAssociationReturnModel>>(ex, "Failed to update Screen Association");
            }
        }
    }
}
