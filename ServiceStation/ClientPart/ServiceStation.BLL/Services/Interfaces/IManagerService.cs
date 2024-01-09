using ServiceStation.BLL.DTO.Responses;

namespace ServiceStation.BLL.Services.Interfaces
{
    public interface IManagerService
    {
        Task<IEnumerable<ManagerResponse>> GetAllAsync();
        Task<ManagerResponse> GetByIdAsync(int id);

    }
}
