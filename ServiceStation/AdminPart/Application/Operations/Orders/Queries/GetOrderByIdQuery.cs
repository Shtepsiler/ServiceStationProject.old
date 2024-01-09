using Application.DTOs.Respponces;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Operations.Orders.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDTO>
    {
        public int Id { get; set; }

    }
    public class GetOrderByIdQueryHendler : IRequestHandler<GetOrderByIdQuery, OrderDTO>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper mapper;

        public GetOrderByIdQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<OrderDTO> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return mapper.Map<Order, OrderDTO>(await _context.Orders.FindAsync(request.Id, cancellationToken));
            }
            catch (Exception ex) { throw ex; }


        }
    }
}
