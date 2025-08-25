using JiJiBotApp_Backend.Controllers.BaseApi;
using JiJiBotApp_Backend.DTOs.Model.Response;
using JiJiBotApp_Backend.DTOs.Model.UserRoleAssociation;
using JiJiBotApp_Backend.DTOs.SearchRequests.UserRoleAssociation;
using JiJiBotApp_Backend.Services.UserRoleAssociation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JiJiBotApp_Backend.Controllers.UserRoleAssociation
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleAssociationController(IUserRoleAssociationService service, ILogger<UserRoleAssociationController> logger) : BaseApiController
    {
        [HttpPost("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserRoleAssociationListModel>>>> Search([FromBody] UserRoleAssociationSearchRequest request)
        {
            try
            {
                var (records, totalCount) = await service.SearchUserRoleAssociation(request);
                return HandleSuccess(records, "UserRoleAssociation list retrieved successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing UserRoleAssociation search request");
                return HandleError<IEnumerable<UserRoleAssociationListModel>>(ex, "Failed to retrieve UserRoleAssociation list");
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<List<dynamic>>>> Add([FromBody] UserRoleAssociationAddRequest request)
        {
            try
            {
                var (id, totalCount) = await service.AddUserRoleAssociation(request);

                List<dynamic> model = new()
                {
                    new { Id = id, totalCount }
                };

                if (id == -1)
                {
                    return HandleSuccess(model, "UserRoleAssociation Already Exists", totalCount);
                }

                return HandleSuccess(model, "UserRoleAssociation added successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing UserRoleAssociation add request");
                return HandleError<List<dynamic>>(ex, "Failed to add UserRoleAssociation");
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete([FromBody] UserRoleAssociationDeleteRequest request)
        {
            try
            {
                var (isDeleted, totalCount) = await service.DeleteUserRoleAssociation(request);

                if (!isDeleted)
                    return HandleSuccess(false, "Failed to delete UserRoleAssociation");

                return HandleSuccess(isDeleted, "UserRoleAssociation deleted successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing UserRoleAssociation delete request");
                return HandleError<bool>(ex, "Failed to delete UserRoleAssociation");
            }
        }

        [HttpPost("edit")]
        public async Task<ActionResult<ApiResponse<List<dynamic>>>> Edit([FromBody] UserRoleAssociationUpdateRequest request)
        {
            try
            {
                var (id, totalCount) = await service.EditUserRoleAssociation(request);

                List<dynamic> model = new()
                {
                    new { Id = id, totalCount }
                };

                if (id == -1)
                {
                    return HandleSuccess(model, "UserRoleAssociation Already Exists", totalCount);
                }

                return HandleSuccess(model, "UserRoleAssociation updated successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing UserRoleAssociation edit request");
                return HandleError<List<dynamic>>(ex, "Failed to update UserRoleAssociation");
            }
        }
    }
}
