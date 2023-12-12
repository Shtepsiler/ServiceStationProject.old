using Application.DTOs.Respponces;
using Application.Operations.Mechanics.Commands;
using MediatR;

namespace Application.Interfaces
{
    public interface IMechanicService
    {
        IMediator Mediator { get; }

        Task Create(CreateMechanicCommand comand);
        Task Delete(int id);
        Task<IEnumerable<MechanicDTO>> GetAllAsync();
        Task<MechanicDTO> GetByIdAsync(int id);
        Task Update(UpdateMechanicCommand comand);
    }
}