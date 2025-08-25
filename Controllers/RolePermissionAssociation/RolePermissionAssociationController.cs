using JiJiBotApp_Backend.Controllers.BaseApi;
using JiJiBotApp_Backend.DTOs.Model.Response;
using JiJiBotApp_Backend.DTOs.Model.RolePermissionAssociation;
using JiJiBotApp_Backend.Services.RolePermissionAssociation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static JiJiBotApp_Backend.DTOs.SearchRequests.RolePermissionAssociation.RolePermissionAssociationRequest;

namespace JiJiBotApp_Backend.Controllers.RolePermissionAssociation
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionAssociationController(IRolePermissionAssociationService rolePermissionAssociationService, ILogger<RolePermissionAssociationController> logger) : BaseApiController
    {
        [HttpPost("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<RolePermissionAssociationListModel>>>> Search([FromBody] RolePermissionAssociationSearchRequest request)
        {
            try
            {
                var (records, totalCount) = await rolePermissionAssociationService.SearchRolePermissionAssociation(request);
                return HandleSuccess(records, "RolePermissionAssociation list retrieved successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing RolePermissionAssociation search request");
                return HandleError<IEnumerable<RolePermissionAssociationListModel>>(ex, "Failed to retrieve RolePermissionAssociation list");
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<List<RolePermissionAssociationReturnModel>>>> Add([FromBody] RolePermissionAssociationAddRequest request)
        {
            try
            {
                var (id, totalCount) = await rolePermissionAssociationService.AddRolePermissionAssociation(request);

                List<RolePermissionAssociationReturnModel> model = new List<RolePermissionAssociationReturnModel>
                {
                    new()
                    {
                        Id = id,
                        TotalCount = totalCount
                    }
                };

                if (id == -1)
                {
                    return HandleSuccess(model, "Association already exists", totalCount);
                }

                return HandleSuccess(model, "RolePermissionAssociation added successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing RolePermissionAssociation add request");
                return HandleError<List<RolePermissionAssociationReturnModel>>(ex, "Failed to add RolePermissionAssociation");
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete([FromBody] RolePermissionAssociationDeleteRequest request)
        {
            try
            {
                var (isDeleted, totalCount) = await rolePermissionAssociationService.DeleteRolePermissionAssociation(request);

                if (!isDeleted)
                    return HandleSuccess(false, "Failed to delete RolePermissionAssociation");

                return HandleSuccess(isDeleted, "RolePermissionAssociation deleted successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing RolePermissionAssociation delete request");
                return HandleError<bool>(ex, "Failed to delete RolePermissionAssociation");
            }
        }

        [HttpPost("edit")]
        public async Task<ActionResult<ApiResponse<List<RolePermissionAssociationReturnModel>>>> Edit([FromBody] RolePermissionAssociationUpdateRequest request)
        {
            try
            {
                var (id, totalCount) = await rolePermissionAssociationService.EditRolePermissionAssociation(request);

                List<RolePermissionAssociationReturnModel> model = new List<RolePermissionAssociationReturnModel>
                {
                    new()
                    {
                        Id = id,
                        TotalCount = totalCount
                    }
                };

                if (id == -1)
                {
                    return HandleSuccess(model, "Association already exists", totalCount);
                }

                return HandleSuccess(model, "RolePermissionAssociation updated successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing RolePermissionAssociation update request");
                return HandleError<List<RolePermissionAssociationReturnModel>>(ex, "Failed to update RolePermissionAssociation");
            }
        }
    }
}
