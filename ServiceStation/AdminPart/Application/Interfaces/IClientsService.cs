using Application.DTOs.Respponces;

namespace Application.Interfaces
{
    public interface IClientsService
    {
        Task<IEnumerable<ClientDTO>> GetAllAsync();
    }
}