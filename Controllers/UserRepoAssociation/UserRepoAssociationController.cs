using JiJiBotApp_Backend.Controllers.BaseApi;
using JiJiBotApp_Backend.DTOs.Model.Response;
using JiJiBotApp_Backend.DTOs.Model.UserRepoAssociation;
using JiJiBotApp_Backend.DTOs.SearchRequests.UserRepoAssociation;
using JiJiBotApp_Backend.Services.UserRepoAssociation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JiJiBotApp_Backend.Controllers.UserRepoAssociation
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRepoAssociationController(IUserRepoAssociationService userRepoAssociationService, ILogger<UserRepoAssociationController> logger) : BaseApiController
    {
        [HttpPost("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserRepoAssociationListModel>>>> Search([FromBody] UserRepoAssociationSearchRequest request)
        {
            try
            {
                var (records, totalCount) = await userRepoAssociationService.SearchUser(request);
                return HandleSuccess(records, "UserRepoAssociation list retrieved successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing search request");
                return HandleError<IEnumerable<UserRepoAssociationListModel>>(ex, "Failed to retrieve UserRepoAssociation list");
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<List<UserRepoAssociationReturnModel>>>> Add([FromBody] UserRepoAssociationAddRequest request)
        {
            try
            {
                var (id, totalCount) = await userRepoAssociationService.AddUser(request);

                List<UserRepoAssociationReturnModel> model = new List<UserRepoAssociationReturnModel>
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

                return HandleSuccess(model, "UserRepoAssociation added successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing add request");
                return HandleError<List<UserRepoAssociationReturnModel>>(ex, "Failed to add UserRepoAssociation");
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete([FromBody] UserRepoAssociationDeleteRequest request)
        {
            try
            {
                var (isDeleted, totalCount) = await userRepoAssociationService.DeleteUser(request);

                if (!isDeleted)
                    return HandleSuccess(false, "Failed to delete UserRepoAssociation");

                return HandleSuccess(isDeleted, "UserRepoAssociation deleted successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing delete request");
                return HandleError<bool>(ex, "Failed to delete UserRepoAssociation");
            }
        }

        [HttpPost("edit")]
        public async Task<ActionResult<ApiResponse<List<UserRepoAssociationReturnModel>>>> Edit([FromBody] UserRepoAssociationUpdateRequest request)
        {
            try
            {
                var (id, totalCount) = await userRepoAssociationService.EditUser(request);

                List<UserRepoAssociationReturnModel> model = new List<UserRepoAssociationReturnModel>
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

                return HandleSuccess(model, "UserRepoAssociation updated successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing edit request");
                return HandleError<List<UserRepoAssociationReturnModel>>(ex, "Failed to update UserRepoAssociation");
            }
        }
    }
}
