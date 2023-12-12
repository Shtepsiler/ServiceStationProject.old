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

namespace Application.Operations.OrderParts.Queries
{
    public class GetOrdersRatrsQuery : IRequest<IEnumerable<OrderPartDTO>>
    {
    }

    public class GetOrdersRatrsQueryHendler : IRequestHandler<GetOrdersRatrsQuery, IEnumerable<OrderPartDTO>>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper _mapper;

        public GetOrdersRatrsQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderPartDTO>> Handle(GetOrdersRatrsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<OrderPart>, IEnumerable<OrderPartDTO>>(await _context.OrderParts.ToListAsync());
        }
    }



}
