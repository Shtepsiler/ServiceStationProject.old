using Application.DTOs.Respponces;
using Application.Operations.PartsNeeded.Commands;
using MediatR;

namespace Application.Interfaces
{
    public interface IPartNeededService
    {
        IMediator Mediator { get; }

        Task Create(CreatePartsNeededCommand comand);
        Task Delete(int id);
        Task<IEnumerable<PartNeededDTO>> GetAllAsync();
        Task<PartNeededDTO> GetByIdAsync(int id);
        Task Update(UpdatePartneededCommand comand);
    }
}