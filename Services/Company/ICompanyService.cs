using JiJiBotApp_Backend.DTOs.Model.Company;
using JiJiBotApp_Backend.DTOs.SearchRequests.Company;

namespace JiJiBotApp_Backend.Services.Company
{
    public interface ICompanyService
    {
        Task<(IEnumerable<CompanyListModel> records, int totalCount)> SearchCompany(SearchCompanyRequest request);
        Task<(int companyId, int totalCount)> AddCompany(AddCompanyRequest request);
        Task<(bool result, int totalCount)> DeleteCompany(DeleteCompanyRequest request);
        Task<(int companyId, int totalCount)> EditCompany(UpdateCompanyRequest request);
    }
}
