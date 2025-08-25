using JiJiBotApp_Backend.DTOs.Model.Company;
using JiJiBotApp_Backend.DTOs.SearchRequests.Company;
using JiJiBotApp_Backend.Repositories.Company;

namespace JiJiBotApp_Backend.Services.Company
{
    public class CompanyService(ICompanyRepository repository, ILogger<CompanyService> logger) : ICompanyService
    {
        public async Task<(IEnumerable<CompanyListModel> records, int totalCount)> SearchCompany(SearchCompanyRequest request)
        {
            logger.LogInformation("Fetching company for type: {request}", request);
            return await repository.SearchCompany(request);
        }
        public async Task<(int companyId, int totalCount)> AddCompany(AddCompanyRequest request)
        {
            logger.LogInformation("Adding Company {request}", request);
            return await repository.AddCompany(request);
        }
        public async Task<(bool result, int totalCount)> DeleteCompany(DeleteCompanyRequest request)
        {
            logger.LogInformation("Delete Company {request}", request);
            return await repository.DeleteCompany(request);
        }
        public async Task<(int companyId, int totalCount)> EditCompany(UpdateCompanyRequest request)
        {
            logger.LogInformation("Updating Company {request}", request);
            return await repository.EditCompany(request);
        }
    }
}
