using ServiceStation.BLL.DTO.Responses;

namespace ServiceStation.BLL.Services.Interfaces
{
    public interface IMechanicService
    {
        Task<MechanicResponse> GetByIdDetailedAsync(int id);
        Task<IEnumerable<MechanicPublicResponse>> GetAllAsync();
    }
}
