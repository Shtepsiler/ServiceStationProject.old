using Application.DTOs.Respponces;
using Application.Operations.MechanicsTasks.Commands;
using MediatR;

namespace Application.Interfaces
{
    public interface IMechanicsTasksService
    {
        IMediator Mediator { get; }

        Task Create(CreateMechanicTaskCommand comand);
        Task Delete(int id);
        Task<IEnumerable<MechanicsTasksDTO>> GetAllAsync();
        Task<IEnumerable<MechanicsTasksDTO>> GetAllByParametrs(int jobId, int mevhanicId);

        Task<MechanicsTasksDTO> GetByIdAsync(int id);
        Task Update(UpdateMechanicTaskCommand comand);
    }
}