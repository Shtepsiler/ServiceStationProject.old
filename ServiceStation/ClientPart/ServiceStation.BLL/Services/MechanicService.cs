using AutoMapper;
using ServiceStation.BLL.DTO.Responses;
using ServiceStation.BLL.Services.Interfaces;
using ServiceStation.DAL.Entities;
using ServiceStation.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.BLL.Services
{
    public class MechanicService : IMechanicService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MechanicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<MechanicPublicResponse>> GetAllAsync()
        {
            try
            {
                return mapper.Map<IEnumerable<Mechanic>, IEnumerable<MechanicPublicResponse>>(await unitOfWork._MechanicRepository.GetAsync());

            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<MechanicResponse> GetByIdDetailedAsync(int id)
        {
            try
            {
                return mapper.Map< Mechanic,MechanicResponse >(await unitOfWork._MechanicRepository.GetByIdAsync(id));

            }
            catch (Exception ex) { throw ex; }
        }
    }
}
