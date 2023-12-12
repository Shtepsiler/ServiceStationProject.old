using AutoMapper;
using ServiceStation.BLL.DTO.Requests;
using ServiceStation.BLL.DTO.Responses;
using ServiceStation.BLL.Mapping;
using ServiceStation.BLL.Services.Interfaces;
using ServiceStation.DAL.Entities;
using ServiceStation.DAL.Repositories;
using ServiceStation.DAL.Repositories.Contracts;

namespace ServiceStation.BLL.Services
{
    public class JobService : IJobService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _maper;

        public JobService(IUnitOfWork unitOfWork, IMapper maper)
        {
            _unitOfWork = unitOfWork;
            _maper = maper;
        }

        public async Task<IEnumerable<JobResponse>> GetAllAsync()
        {
            try
            {
                var results =(List<Job>) await _unitOfWork._JobRepository.GetAsync();
                return _maper.Map<List<Job>, List<JobResponse>>(results);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IEnumerable<ClientsJobsResponse>> GetAllClientsJobsAsync(int clientId)
        {
            try
            {
                var results = (List<Job>)await _unitOfWork._JobRepository.GetByClientIdAsync(clientId);

                return  _maper.Map<List<Job>,List<ClientsJobsResponse>>(results);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<JobResponse> GetByIdAsync(int id)
        {
            try
            {
                var result = await _unitOfWork._JobRepository.GetByIdAsync(id);

                if (result == null)
                {
                    return null;
                }
                else
                {
                    return _maper.Map<Job, JobResponse>(result);
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task PostAsync(JobRequest job)
        {
            try
            {

                await _unitOfWork._JobRepository.InsertAsync(_maper.Map<JobRequest, Job>(job));
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task PostNewJobAsync(NewJobRequest job)
        {
            Model inmodel;
            try
            {
                Job JOB = new Job { 
                    ClientId = job.ClientId,
                IssueDate = job.IssueDate,
                Description = job.Description  
                };
                var model = _unitOfWork._ModelRepository.GetModelByName(job.ModelName);
                if(model.Result == null)
                {
                    inmodel =  new Model(job.ModelName);
                    _unitOfWork._ModelRepository.InsertAsync(inmodel);
                    await _unitOfWork.SaveChangesAsync();

                    JOB.Model = inmodel;

                }
                else
                {
                    JOB.Model = model.Result;

                }
                _unitOfWork._JobRepository.InsertAsync(JOB);
                await _unitOfWork.SaveChangesAsync();



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(int id, JobRequest job)
        {

            try
            {


                var event_entity = await _unitOfWork._JobRepository.GetByIdAsync(id);
                if (event_entity == null)
                {

                }

                await _unitOfWork._JobRepository.UpdateAsync(_maper.Map<JobRequest, Job>(job));
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                var event_entity = await _unitOfWork._JobRepository.GetByIdAsync(id);
                if (event_entity == null)
                {
                    throw new Exception();

                }

                await _unitOfWork._JobRepository.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
               
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


    }
}
