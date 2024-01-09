using ServiceStation.BLL.DTO.Requests;
using ServiceStation.BLL.DTO.Responses;

namespace ServiceStation.BLL.Services.Interfaces
{
    public interface IModelService
    {
        Task<IEnumerable<ModelResponse>> GetAllAsync();
        Task<ModelResponse> GetByIdAsync(int id);
        Task PostAsync(ModelRequest model);
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(int id, ModelRequest model);


    }
}
