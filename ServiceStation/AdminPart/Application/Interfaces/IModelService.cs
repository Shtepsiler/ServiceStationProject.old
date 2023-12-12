using Application.DTOs.Respponces;
using Application.Operations.Models.Commands;
using MediatR;

namespace Application.Interfaces
{
    public interface IModelService
    {
        IMediator Mediator { get; }

        Task Create(CreateModelCommand comand);
        Task Delete(int id);
        Task<IEnumerable<ModelDTO>> GetAllAsync();
        Task<ModelDTO> GetByIdAsync(int id);
        Task Update(UpdateModelCommand comand);
    }
}