using AutoMapper;
using ServiceStation.BLL.DTO.Requests;
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
    public class ManagerService : IManagerService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _maper;

        public ManagerService(IUnitOfWork unitOfWork, IMapper maper)
        {
            _unitOfWork = unitOfWork;
            _maper = maper;
        }

        public async Task<IEnumerable<ManagerResponse>> GetAllAsync()
        {
            List<Manager> results;
            try
            {
                results = (List<Manager>) await _unitOfWork._ManagerRepository.GetAsync();
         
            }
            catch (Exception ex)
            {
                return null;
            }    
            return _maper.Map<List<Manager>,List<ManagerResponse>>(results);
        }

        public async Task<ManagerResponse> GetByIdAsync(int id)
        {
            try
            {
                var result = await _unitOfWork._ManagerRepository.GetByIdAsync(id);
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return _maper.Map<Manager, ManagerResponse>(result);
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }


       

    }
}
