using Microsoft.AspNetCore.Mvc;
using JiJiBotApp_Backend.Controllers.BaseApi;
using JiJiBotApp_Backend.DTOs.Model.Company;
using JiJiBotApp_Backend.DTOs.Model.Response;
using JiJiBotApp_Backend.DTOs.SearchRequests.Company;
using JiJiBotApp_Backend.Services.Company;

namespace JiJiBotApp_Backend.Controllers.Company
{
    public class CompanyController(ICompanyService companyService, ILogger<CompanyController> logger) : BaseApiController
    {
        [HttpPost("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<CompanyListModel>>>> Search([FromBody] SearchCompanyRequest request)
        {
            try
            {
                var (records, totalCount) = await companyService.SearchCompany(request);
                return HandleSuccess(records, "Company List retrieved successfully", totalCount);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error processing dropdown request");
                return HandleError<IEnumerable<CompanyListModel>>(ex, "Failed to retrieve Company List");
            }
        }
        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<List<CopmanyReturnModel>>>> AddCompany([FromBody] AddCompanyRequest request)
        {
            try
            {
                var (companyId, totalCount) = await companyService.AddCompany(request);

                List<CopmanyReturnModel> model = new List<CopmanyReturnModel>
        {
            new()
            {
                CompanyId = companyId,
                totalCount = totalCount
            }
        };
                if (companyId == -1)
                {
                    return HandleSuccess(model, "Company Name or Code Already Exists", totalCount);
                }
                else
                {
                    return HandleSuccess(model, "Company added successfully", totalCount);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing add request");
                return HandleError<List<CopmanyReturnModel>>(ex, "Failed to add Company");
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteCompany([FromBody] DeleteCompanyRequest request)
        {
            try
            {
                (bool isDeleted,int totalCount) = await companyService.DeleteCompany(request);

                if (!isDeleted)
                    return HandleSuccess<bool>(false, "Failed to Delete Company");

                return HandleSuccess(isDeleted, "Company Deleted successfully", totalCount);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing Delete request");
                return HandleError<bool>(ex, "Failed to delete Company");
            }
        }

        [HttpPost("edit")]
        public async Task<ActionResult<ApiResponse<List<CopmanyReturnModel>>>> EditCompany([FromBody] UpdateCompanyRequest request)
        {
            try
            {
                var (companyId, totalCount) = await companyService.EditCompany(request);

                List<CopmanyReturnModel> model = new List<CopmanyReturnModel>
        {
            new()
            {
                CompanyId = companyId,
                totalCount = totalCount
            }
        };
                if (companyId == -1)
                {
                    return HandleSuccess(model, "Company Name or Code Already Exists", totalCount);
                }
                else
                {
                    return HandleSuccess(model, "Company updated successfully", totalCount);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing add request");
                return HandleError<List<CopmanyReturnModel>>(ex, "Failed to updated Company");
            }
        }

    }
}
