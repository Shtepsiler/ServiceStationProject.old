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

namespace Application.Operations.Models.Queries
{
    public class GetModelByIdQuery : IRequest<ModelDTO>
    {
        public int Id { get; set; }

    }
    public class GetModelByIdQueryHendler : IRequestHandler<GetModelByIdQuery, ModelDTO>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper mapper;

        public GetModelByIdQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<ModelDTO> Handle(GetModelByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return mapper.Map<Model, ModelDTO>(await _context.Models.FindAsync(request.Id, cancellationToken));
            }
            catch (Exception ex) { throw ex; }


        }
    }
}
