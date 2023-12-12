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
    public class GetModelsQuery : IRequest<IEnumerable<ModelDTO>>
    {
    }

    public class GetModelsQueryHendler : IRequestHandler<GetModelsQuery, IEnumerable<ModelDTO>>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper _mapper;

        public GetModelsQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ModelDTO>> Handle(GetModelsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Model>, IEnumerable<ModelDTO>>(await _context.Models.ToListAsync());
        }
    }



}
