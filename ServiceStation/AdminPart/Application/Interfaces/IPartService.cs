using Application.DTOs.Respponces;
using Application.Operations.Parts.Commands;
using MediatR;

namespace Application.Interfaces
{
    public interface IPartService
    {
        IMediator Mediator { get; }

        Task Create(CreatePartCommand comand);
        Task Delete(int id);
        Task<IEnumerable<PartDTO>> GetAllAsync();
        Task<PartDTO> GetByIdAsync(int id);
        Task Update(UpdatePartCommand comand);
    }
}