using Application.DTOs.Respponces;
using Application.Operations.Jobs.Commands;

namespace Application.Interfaces
{
    public interface IJobService
    {
        Task Create(CreateJobCommand comand);
        Task Delete(int id);
        Task<IEnumerable<JobDTO>> GetAllAsync();
        Task<JobDTO> GetByIdAsync(int id);
        Task Update(UpdateJobCommand comand);
        Task<IEnumerable<JobDTO>> GetByIssueDateAsync(DateTime IssueDate);
    }

}