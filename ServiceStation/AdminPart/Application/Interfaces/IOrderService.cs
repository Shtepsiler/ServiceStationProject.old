using Application.DTOs.Respponces;
using Application.Operations.Orders.Commands;
using MediatR;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        IMediator Mediator { get; }

        Task Create(CreateOrderCommand comand);
        Task Delete(int id);
        Task<IEnumerable<OrderDTO>> GetAllAsync();
        Task<OrderDTO> GetByIdAsync(int id);
        Task Update(UpdateOrderCommand comand);
    }
}