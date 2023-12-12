using ServiceStation.BLL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.BLL.Services.Interfaces
{
    public interface IMechanicService
    {
        Task<MechanicResponse> GetByIdDetailedAsync(int id);
        Task<IEnumerable<MechanicPublicResponse>> GetAllAsync();
    }
}
