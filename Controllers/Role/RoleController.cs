using JiJiBotApp_Backend.Controllers.BaseApi;
using JiJiBotApp_Backend.DTOs.Model.Response;
using JiJiBotApp_Backend.DTOs.Model.Role;
using JiJiBotApp_Backend.Services.Role;
using Microsoft.AspNetCore.Mvc;
using JiJiBotApp_Backend.DTOs.SearchRequests.Role;

namespace JiJiBotApp_Backend.Controllers.Role
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

                return HandleSuccess(model, "Role and permissions added successfully", totalCount);
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

                return HandleSuccess(isDeleted, "Role and associated permissions deleted successfully", totalCount);
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

                return HandleSuccess(model, "Role and permissions updated successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing Role update request");
                return HandleError<List<RoleReturnModel>>(ex, "Failed to update Role");
            }
        }

        // New endpoint for managing role permissions independently
        [HttpPost("permissions/update")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateRolePermissions([FromBody] RolePermissionsUpdateRequest request)
        {
            try
            {
                var (result, totalCount) = await roleService.UpdateRolePermissions(request);

                if (!result)
                    return HandleSuccess(false, "Failed to update role permissions");

                return HandleSuccess(result, "Role permissions updated successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing role permissions update request");
                return HandleError<bool>(ex, "Failed to update role permissions");
            }
        }

        // New endpoint to get permissions for a specific role
        [HttpGet("{roleId}/permissions")]
        public async Task<ActionResult<ApiResponse<List<RolePermissionModel>>>> GetRolePermissions(int roleId)
        {
            try
            {
                var (permissions, totalCount) = await roleService.GetRolePermissions(roleId);
                return HandleSuccess(permissions, "Role permissions retrieved successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving role permissions for roleId: {roleId}", roleId);
                return HandleError<List<RolePermissionModel>>(ex, "Failed to retrieve role permissions");
            }
        }

        // New endpoint to get all available permissions
        [HttpGet("permissions/available")]
        public async Task<ActionResult<ApiResponse<IEnumerable<RolePermissionModel>>>> GetAvailablePermissions()
        {
            try
            {
                var (permissions, totalCount) = await roleService.GetAvailablePermissions();
                return HandleSuccess(permissions, "Available permissions retrieved successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving available permissions");
                return HandleError<IEnumerable<RolePermissionModel>>(ex, "Failed to retrieve available permissions");
            }
        }
    }
}