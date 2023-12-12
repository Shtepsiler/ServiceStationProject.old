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

namespace Application.Operations.Vendors.Queries
{
    public class GetVendorsQuery : IRequest<IEnumerable<VendorDTO>>
    {
    }

    public class GetVendorsQueryHendler : IRequestHandler<GetVendorsQuery, IEnumerable<VendorDTO>>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper _mapper;

        public GetVendorsQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VendorDTO>> Handle(GetVendorsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Vendor>, IEnumerable<VendorDTO>>(await _context.Vendors.ToListAsync());
        }
    }



}
