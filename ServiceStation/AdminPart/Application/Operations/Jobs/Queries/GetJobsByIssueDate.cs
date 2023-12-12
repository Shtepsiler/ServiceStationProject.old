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
    public class GetJobsByIssueDate : IRequest<IEnumerable<JobDTO>>
    {
        public DateTime IssueDate { get; set; }
    }
    public class GetJobsByIssueDateHendler : IRequestHandler<GetJobsByIssueDate, IEnumerable<JobDTO>>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper _mapper;

        public GetJobsByIssueDateHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JobDTO>> Handle(GetJobsByIssueDate request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Job>, IEnumerable<JobDTO>>(await _context.Jobs.Where(p => p.IssueDate.Date == request.IssueDate.Date).ToListAsync());
        }
    }
}
