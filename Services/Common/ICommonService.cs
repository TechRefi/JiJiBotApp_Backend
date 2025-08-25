using JiJiBotApp_Backend.DTOs.Model.Common;
using JiJiBotApp_Backend.DTOs.SearchRequests.Common;

namespace JiJiBotApp_Backend.Services.Common
{
    public interface ICommonService
    {
        Task<List<DropdownItem>> GetDropdownItemsAsync(DropdownRequest request);
        Task<bool> ValidateDropdownRequestAsync(DropdownRequest request);

    }
}
