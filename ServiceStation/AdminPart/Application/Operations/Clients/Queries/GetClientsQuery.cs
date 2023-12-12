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

namespace Application.Operations.Clients.Queries
{
    public class GetClientsQuery : IRequest<IEnumerable<ClientDTO>>
    {
    }

    public class GetClientsQueryHendler : IRequestHandler<GetClientsQuery, IEnumerable<ClientDTO>>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper _mapper;

        public GetClientsQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDTO>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Client>, IEnumerable<ClientDTO>>(await _context.Clients.ToListAsync());
        }
    }
}