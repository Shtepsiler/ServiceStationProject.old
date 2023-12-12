using Microsoft.AspNetCore.Mvc;
using ServiceStation.DAL.Entities;

using ServiceStation.BLL.Services.Interfaces;
using ServiceStation.BLL.DTO.Responses;
using ServiceStation.BLL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;

namespace ServiceStation.API.Controllers
{
    //видалити

    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {

        private IUnitOfBisnes _UnitOfBisnes;

        private readonly ILogger<ManagerController> _logger;
        public ManagerController(
            ILogger<ManagerController> logger,
             IUnitOfBisnes UnitOfBisnes
            )
        {
            _logger = logger;
            _UnitOfBisnes = UnitOfBisnes;
        }

        //GET: api/jobs
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<ManagerResponse>>> GetAllAsync()
        {
            try
            {
                var results = await _UnitOfBisnes._ManagerService.GetAllAsync();


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
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<ManagerResponse>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _UnitOfBisnes._ManagerService.GetByIdAsync(id);
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

     

    }
}
