using JiJiBotApp_Backend.Controllers.BaseApi;
using JiJiBotApp_Backend.DTOs.Model.Response;
using JiJiBotApp_Backend.DTOs.Model.Role;
using JiJiBotApp_Backend.Services.Role;
using Microsoft.AspNetCore.Mvc;
using JiJiBotApp_Backend.DTOs.SearchRequests.Role;


namespace TechRiFi.Controllers.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(IRoleService roleService, ILogger<RoleController> logger) : BaseApiController
    {
        [HttpPost("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<RoleListModel>>>> Search([FromBody] RoleSearchRequest request)
        {
            try
            {
                var (records, totalCount) = await roleService.SearchRole(request);
                return HandleSuccess(records, "Role List retrieved successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing Role search request");
                return HandleError<IEnumerable<RoleListModel>>(ex, "Failed to retrieve Role List");
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<List<RoleReturnModel>>>> AddRole([FromBody] RoleAddRequest request)
        {
            try
            {
                var (roleId, totalCount) = await roleService.AddRole(request);

                List<RoleReturnModel> model = new List<RoleReturnModel>
                {
                    new()
                    {
                        RoleId = roleId,
                        TotalCount = totalCount
                    }
                };

                if (roleId == -1)
                {
                    return HandleSuccess(model, "Role Name or Code Already Exists", totalCount);
                }

                return HandleSuccess(model, "Role added successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing Role add request");
                return HandleError<List<RoleReturnModel>>(ex, "Failed to add Role");
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteRole([FromBody] RoleDeleteRequest request)
        {
            try
            {
                var (isDeleted, totalCount) = await roleService.DeleteRole(request);

                if (!isDeleted)
                    return HandleSuccess(false, "Failed to delete Role");

                return HandleSuccess(isDeleted, "Role deleted successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing Role delete request");
                return HandleError<bool>(ex, "Failed to delete Role");
            }
        }

        [HttpPost("edit")]
        public async Task<ActionResult<ApiResponse<List<RoleReturnModel>>>> EditRole([FromBody] RoleUpdateRequest request)
        {
            try
            {
                var (roleId, totalCount) = await roleService.EditRole(request);

                List<RoleReturnModel> model = new List<RoleReturnModel>
                {
                    new()
                    {
                        RoleId = roleId,
                        TotalCount = totalCount
                    }
                };

                if (roleId == -1)
                {
                    return HandleSuccess(model, "Role Name or Code Already Exists", totalCount);
                }

                return HandleSuccess(model, "Role updated successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing Role update request");
                return HandleError<List<RoleReturnModel>>(ex, "Failed to update Role");
            }
        }
    }
}
