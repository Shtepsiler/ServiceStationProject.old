using Application.DTOs.Respponces;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Orders.Queries
{
    public class GetOrdersQuery : IRequest<IEnumerable<OrderDTO>>
    {
    }

    public class GetOrdersQueryHendler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDTO>>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper _mapper;

        public GetOrdersQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDTO>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(await _context.Orders.ToListAsync());
        }
    }



}
