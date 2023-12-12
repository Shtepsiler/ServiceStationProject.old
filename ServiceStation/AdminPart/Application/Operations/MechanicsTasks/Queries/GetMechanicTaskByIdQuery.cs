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

namespace Application.Operations.MechanicsTasks.Queries
{
    public class GetMechanicTaskByIdQuery : IRequest<MechanicsTasksDTO>
    {
        public int Id { get; set; }

    }
    public class GetMechanicTaskByIdQueryHendler : IRequestHandler<GetMechanicTaskByIdQuery, MechanicsTasksDTO>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper mapper;

        public GetMechanicTaskByIdQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<MechanicsTasksDTO> Handle(GetMechanicTaskByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return mapper.Map<Domain.Entities.MechanicsTasks, MechanicsTasksDTO>(await _context.MechanicsTasks.FindAsync(request.Id, cancellationToken));
            }
            catch (Exception ex) { throw ex; }


        }
    }
}
