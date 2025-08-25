using JiJiBotApp_Backend.DTOs.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JiJiBotApp_Backend.Controllers.BaseApi
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] // This ensures all derived controllers require JWT authentication by default
    public abstract class BaseApiController : ControllerBase
    {
        protected ActionResult<ApiResponse<T>> HandleSuccess<T>(T data, string message = "Success", int totalCount = 0)
        {
            return Ok(ApiResponse<T>.SuccessResponse(data, message, totalCount));
        }

        protected ActionResult<ApiResponse<T>> HandleError<T>(Exception ex, string friendlyMessage = "An error occurred")
        {
            // Log the error in the derived controller
            return StatusCode(500, ApiResponse<T>.ErrorResponse(friendlyMessage));
        }
    }
}
