using JiJiBotApp_Backend.Controllers.BaseApi;
using JiJiBotApp_Backend.DTOs.Model.Repositories;
using JiJiBotApp_Backend.DTOs.Model.Response;
using JiJiBotApp_Backend.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static JiJiBotApp_Backend.DTOs.SearchRequests.Repositories.RepositoryRequest;

namespace JiJiBotApp_Backend.Controllers.Repositories
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoriesController(IRepositoriesService repositoriesService, ILogger<RepositoriesController> logger) : BaseApiController
    {
        [HttpPost("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<RepositoriesListModel>>>> SearchRepository([FromBody] SearchRepositoryRequest request)
        {
            try
            {
                var (records, totalCount) = await repositoriesService.SearchRepository(request);
                return HandleSuccess(records, "Repositories list retrieved successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing search request");
                return HandleError<IEnumerable<RepositoriesListModel>>(ex, "Failed to retrieve repositories list");
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<List<RepositoriesReturnModel>>>> AddRepository([FromBody] AddRepositoryRequest request)
        {
            try
            {
                var (repositoryId, totalCount) = await repositoriesService.AddRepository(request);

                List<RepositoriesReturnModel> model = new()
                {
                    new()
                    {
                        RepositoriesId = repositoryId,
                        TotalCount = totalCount
                    }
                };

                if (repositoryId == -1)
                {
                    return HandleSuccess(model, "Repository name already exists", totalCount);
                }
                else
                {
                    return HandleSuccess(model, "Repository added successfully", totalCount);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing add request");
                return HandleError<List<RepositoriesReturnModel>>(ex, "Failed to add repository");
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteRepository([FromBody] DeleteRepositoryRequest request)
        {
            try
            {
                var (isDeleted, totalCount) = await repositoriesService.DeleteRepository(request);

                if (!isDeleted)
                    return HandleSuccess(false, "Failed to delete repository");

                return HandleSuccess(isDeleted, "Repository deleted successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing delete request");
                return HandleError<bool>(ex, "Failed to delete repository");
            }
        }

        [HttpPost("edit")]
        public async Task<ActionResult<ApiResponse<List<RepositoriesReturnModel>>>> EditRepository([FromBody] UpdateRepositoryRequest request)
        {
            try
            {
                var (repositoryId, totalCount) = await repositoriesService.EditRepository(request);

                List<RepositoriesReturnModel> model = new()
                {
                    new()
                    {
                        RepositoriesId = repositoryId,
                        TotalCount = totalCount
                    }
                };

                if (repositoryId == -1)
                {
                    return HandleSuccess(model, "Repository name already exists", totalCount);
                }
                else
                {
                    return HandleSuccess(model, "Repository updated successfully", totalCount);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing edit request");
                return HandleError<List<RepositoriesReturnModel>>(ex, "Failed to update repository");
            }
        }
    }
}
