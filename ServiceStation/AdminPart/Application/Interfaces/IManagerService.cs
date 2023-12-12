using Application.DTOs.Respponces;
using Application.Operations.Managers.Commands;
using MediatR;

namespace Application.Interfaces
{
    public interface IManagerService
    {
        IMediator Mediator { get; }

        Task Create(CreateManagerCommand comand);
        Task Delete(int id);
        Task<IEnumerable<ManagerDTO>> GetAllAsync();
        Task<ManagerDTO> GetByIdAsync(int id);
        Task Update(UpdateManagerCommand comand);
    }
}