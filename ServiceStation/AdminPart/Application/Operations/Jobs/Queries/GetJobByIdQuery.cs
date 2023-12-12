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

namespace Application.Operations.Jobs.Queries
{
    public class GetJobByIdQuery : IRequest<JobDTO>
    {
        public int Id { get; set; }

    }
    public class GetJobByIdQueryHendler : IRequestHandler<GetJobByIdQuery, JobDTO>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper mapper;

        public GetJobByIdQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<JobDTO> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return mapper.Map<Job, JobDTO>(await _context.Jobs.FindAsync(request.Id, cancellationToken));
            }
            catch (Exception ex) { throw ex; }


        }
    }
}
