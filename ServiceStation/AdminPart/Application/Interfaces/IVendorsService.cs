using Application.DTOs.Respponces;
using Application.Operations.Vendors.Commands;
using MediatR;

namespace Application.Interfaces
{
    public interface IVendorsService
    {
        IMediator Mediator { get; }

        Task Create(CreateVendorCommand comand);
        Task Delete(int id);
        Task<IEnumerable<VendorDTO>> GetAllAsync();
        Task<VendorDTO> GetByIdAsync(int id);
        Task Update(UpdateVendorCommand comand);
    }
}