using Microsoft.AspNetCore.Mvc;
using ServiceStation.DAL.Entities;

using ServiceStation.BLL.Services.Interfaces;
using ServiceStation.BLL.DTO.Responses;
using ServiceStation.BLL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ServiceStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {

        private IUnitOfBisnes _UnitOfBisnes;

        private readonly ILogger<JobController> _logger;
        public JobController(
            ILogger<JobController> logger,
             IUnitOfBisnes UnitOfBisnes
            )
        {
            _logger = logger;
            _UnitOfBisnes = UnitOfBisnes;
        }

        //GET: api/jobs
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobResponse>>> GetAllAsync()
        {
            try
            {
                var results = await _UnitOfBisnes._JobService.GetAllAsync();


                _logger.LogInformation($"Отримали всі івенти з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllEventsAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //GET: api/jobs/Id
        [Authorize]
        [HttpGet("{Id}")]
        public async Task<ActionResult<JobResponse>> GetByIdAsync(int Id)
        {
            try
            {
                var result = await _UnitOfBisnes._JobService.GetByIdAsync(Id);
                if (result == null)
                {
                    _logger.LogInformation($"Івент із Id: {Id}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали івент з бази даних!");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllEventsAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //GET: api/jobs/Id
        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<ActionResult<ClientsJobsResponse>> GetByUserIdAsync(int id)
        {
            try
            {
                var result = await _UnitOfBisnes._JobService.GetAllClientsJobsAsync(id);
                if (result == null)
                {
                    _logger.LogInformation($"Івент із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали івент з бази даних!");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllEventsAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //POST: api/jobs
        /*  [HttpPost]
          public async Task<ActionResult> PostAsync([FromBody] JobRequest job)
          {
              try
              {
                  if (job == null)
                  {
                      _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                      return BadRequest("Обєкт івенту є null");
                  }
                  if (!ModelState.IsValid)
                  {
                      _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                      return BadRequest("Обєкт івенту є некоректним");
                  }
                  await _UnitOfBisnes._JobService.PostAsync(job);
                  return StatusCode(StatusCodes.Status201Created);
              }
              catch (Exception ex)
              {
                  _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі PostAsync - {ex.Message}");
                  return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
              }
          }*/
        //POST: api/jobs
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostNewAsync([FromBody] NewJobRequest job)
        {
            try
            {
                if (job == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт івенту є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт івенту є некоректним");
                }
                await _UnitOfBisnes._JobService.PostNewJobAsync(job);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі PostNewAsync - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //POST: api/jobs/Id
        [Authorize]
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] JobRequest job)
        {
            try
            {
                if (job == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт івенту є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт івенту є некоректним");
                }
                job.Id = id;

                await _UnitOfBisnes._JobService.UpdateAsync(id, job);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі PostAsync - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //GET: api/jobs/Id
        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            try
            {
                var event_entity = await _UnitOfBisnes._JobService.GetByIdAsync(id);
                if (event_entity == null)
                {
                    _logger.LogInformation($"Запис із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }

                await _UnitOfBisnes._JobService.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
