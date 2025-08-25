using JiJiBotApp_Backend.Controllers.BaseApi;
using JiJiBotApp_Backend.DTOs.Model.Response;
using JiJiBotApp_Backend.DTOs.Model.User;
using JiJiBotApp_Backend.DTOs.SearchRequests.User;
using JiJiBotApp_Backend.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JiJiBotApp_Backend.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
   
     
        public class UserController(IUserService userService, ILogger<UserController> logger) : BaseApiController
        {
            [HttpPost("search")]
            public async Task<ActionResult<ApiResponse<IEnumerable<UserListModel>>>> Search([FromBody] UserSearchRequest request)
            {
                try
                {
                    var (records, totalCount) = await userService.SearchUser(request);
                    return HandleSuccess(records, "User List retrieved successfully", totalCount);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error processing search request");
                    return HandleError<IEnumerable<UserListModel>>(ex, "Failed to retrieve User List");
                }
            }

            [HttpPost("add")]
            public async Task<ActionResult<ApiResponse<List<UserReturnModel>>>> AddUser([FromBody] UserAddRequest request)
            {
                try
                {
                    var (userId, totalCount) = await userService.AddUser(request);

                    List<UserReturnModel> model = new List<UserReturnModel>
                {
                    new()
                    {
                        UserId = userId,
                        totalCount = totalCount
                    }
                };

                    if (userId == -1)
                    {
                        return HandleSuccess(model, "User Email or Username Already Exists", totalCount);
                    }

                    return HandleSuccess(model, "User added successfully", totalCount);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error processing add request");
                    return HandleError<List<UserReturnModel>>(ex, "Failed to add User");
                }
            }

            [HttpPost("delete")]
            public async Task<ActionResult<ApiResponse<bool>>> DeleteUser([FromBody] UserDeleteRequest request)
            {
                try
                {
                    var (isDeleted, totalCount) = await userService.DeleteUser(request);

                    if (!isDeleted)
                        return HandleSuccess(false, "Failed to Delete User");

                    return HandleSuccess(isDeleted, "User deleted successfully", totalCount);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error processing delete request");
                    return HandleError<bool>(ex, "Failed to delete User");
                }
            }

            [HttpPost("edit")]
            public async Task<ActionResult<ApiResponse<List<UserReturnModel>>>> EditUser([FromBody] UserUpdateRequest request)
            {
                try
                {
                    var (userId, totalCount) = await userService.EditUser(request);

                    List<UserReturnModel> model = new List<UserReturnModel>
                {
                    new()
                    {
                        UserId = userId,
                        totalCount = totalCount
                    }
                };

                    if (userId == -1)
                    {
                        return HandleSuccess(model, "User Email or Username Already Exists", totalCount);
                    }

                    return HandleSuccess(model, "User updated successfully", totalCount);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error processing edit request");
                    return HandleError<List<UserReturnModel>>(ex, "Failed to update User");
                }
            }
        }

    
}
