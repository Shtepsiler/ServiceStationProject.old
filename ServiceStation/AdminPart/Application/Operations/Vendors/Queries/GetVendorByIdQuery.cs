using Application.DTOs.Respponces;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Operations.Vendors.Queries
{
    public class GetVendorByIdQuery : IRequest<VendorDTO>
    {
        public int Id { get; set; }

    }
    public class GetVendorByIdQueryHendler : IRequestHandler<GetVendorByIdQuery, VendorDTO>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper mapper;

        public GetVendorByIdQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<VendorDTO> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return mapper.Map<Vendor, VendorDTO>(await _context.Vendors.FindAsync(request.Id, cancellationToken));
            }
            catch (Exception ex) { throw ex; }


        }
    }
}
