using JiJiBotApp_Backend.DTOs.Model.Common;
using JiJiBotApp_Backend.DTOs.SearchRequests.Common;

namespace JiJiBotApp_Backend.Repositories.Common
{
    public interface ICommonRepository
    {
        Task<List<DropdownItem>> GetDropdownItemsAsync(DropdownRequest request);
    }
}
