using JiJiBotApp_Backend.Controllers.BaseApi;
using JiJiBotApp_Backend.DTOs.Model.Permission;
using JiJiBotApp_Backend.DTOs.Model.Response;
using JiJiBotApp_Backend.DTOs.SearchRequests.Permission;
using JiJiBotApp_Backend.Services.Permission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static JiJiBotApp_Backend.DTOs.SearchRequests.Permission.PermissionRequest;

namespace JiJiBotApp_Backend.Controllers.Permission
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController(IPermissionService permissionService, ILogger<PermissionController> logger) : BaseApiController
    {
        [HttpPost("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<PermissionListModel>>>> Search([FromBody] SearchPermissionRequest request)
        {
            try
            {
                var (records, totalCount) = await permissionService.SearchPermission(request);
                return HandleSuccess(records, "Permission list retrieved successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing search request");
                return HandleError<IEnumerable<PermissionListModel>>(ex, "Failed to retrieve Permission list");
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<List<PermissionReturnModel>>>> AddPermission([FromBody] AddPermissionRequest request)
        {
            try
            {
                var (permissionId, totalCount) = await permissionService.AddPermission(request);

                List<PermissionReturnModel> model = new()
                {
                    new()
                    {
                        PermissionId = permissionId,
                        totalCount = totalCount
                    }
                };

                if (permissionId == -1)
                {
                    return HandleSuccess(model, "Permission already exists", totalCount);
                }

                return HandleSuccess(model, "Permission added successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing add request");
                return HandleError<List<PermissionReturnModel>>(ex, "Failed to add Permission");
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePermission([FromBody] DeletePermissionRequest request)
        {
            try
            {
                var (isDeleted, totalCount) = await permissionService.DeletePermission(request);

                if (!isDeleted)
                    return HandleSuccess(false, "Failed to delete Permission");

                return HandleSuccess(isDeleted, "Permission deleted successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing delete request");
                return HandleError<bool>(ex, "Failed to delete Permission");
            }
        }

        [HttpPost("edit")]
        public async Task<ActionResult<ApiResponse<List<PermissionReturnModel>>>> EditPermission([FromBody] UpdatePermissionRequest request)
        {
            try
            {
                var (permissionId, totalCount) = await permissionService.EditPermission(request);

                List<PermissionReturnModel> model = new()
                {
                    new()
                    {
                        PermissionId = permissionId,
                        totalCount = totalCount
                    }
                };

                if (permissionId == -1)
                {
                    return HandleSuccess(model, "Permission already exists", totalCount);
                }

                return HandleSuccess(model, "Permission updated successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing edit request");
                return HandleError<List<PermissionReturnModel>>(ex, "Failed to update Permission");
            }
        }
    }
}
