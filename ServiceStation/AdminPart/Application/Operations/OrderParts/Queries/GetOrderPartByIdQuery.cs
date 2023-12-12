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
    public class GetOrderPartByIdQuery : IRequest<OrderPartDTO>
    {
        public int Id { get; set; }

    }
    public class GetOrderPartByIdQueryHendler : IRequestHandler<GetOrderPartByIdQuery, OrderPartDTO>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper mapper;

        public GetOrderPartByIdQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<OrderPartDTO> Handle(GetOrderPartByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return mapper.Map<OrderPart, OrderPartDTO>(await _context.OrderParts.FindAsync(request.Id, cancellationToken));
            }
            catch (Exception ex) { throw ex; }


        }
    }
}