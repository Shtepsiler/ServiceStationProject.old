using Application.DTOs.Respponces;
using Application.Operations.OrderParts.Commands;
using MediatR;

namespace Application.Interfaces
{
    public interface IOrderPartsService
    {
        IMediator Mediator { get; }

        Task Create(CreateOrderPartCommand comand);
        Task Delete(int id);
        Task<IEnumerable<OrderPartDTO>> GetAllAsync();
        Task<OrderPartDTO> GetByIdAsync(int id);
        Task Update(UpdateOrderPartCommand comand);
    }
}