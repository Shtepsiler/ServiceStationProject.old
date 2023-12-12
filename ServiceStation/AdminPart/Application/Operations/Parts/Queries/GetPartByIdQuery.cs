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

namespace Application.Operations.Parts.Queries
{
    public class GetPartByIdQuery : IRequest<PartDTO>
    {
        public int Id { get; set; }

    }
    public class GetPartByIdQueryHendler : IRequestHandler<GetPartByIdQuery, PartDTO>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper mapper;

        public GetPartByIdQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<PartDTO> Handle(GetPartByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return mapper.Map<Part, PartDTO>(await _context.Parts.FindAsync(request.Id, cancellationToken));
            }
            catch (Exception ex) { throw ex; }


        }
    }
}
