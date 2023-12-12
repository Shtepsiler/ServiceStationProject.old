
using ServiceStation.BLL.DTO.Requests;
using ServiceStation.BLL.DTO.Responses;
using ServiceStation.DAL.Entities;

namespace ServiceStation.BLL.Services.Interfaces
{
    public interface IJobService
    {
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(int id, JobRequest job);
        Task PostAsync(JobRequest job);
        Task<JobResponse> GetByIdAsync(int id);
        Task<IEnumerable<JobResponse>> GetAllAsync();
        Task<IEnumerable<ClientsJobsResponse>> GetAllClientsJobsAsync(int clientId);
        Task PostNewJobAsync(NewJobRequest job);

    }
}
