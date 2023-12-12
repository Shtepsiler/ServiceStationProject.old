using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceStation.BLL.DTO.Responses;
using ServiceStation.BLL.Services.Interfaces;

namespace ServiceStation.API.Controllers
{
    //видалити
    [Route("api/[controller]")]
    [ApiController]
    public class MechanicController : ControllerBase
    {



        private IUnitOfBisnes _UnitOfBisnes;

        private readonly ILogger<MechanicController> _logger;
        public MechanicController(
            ILogger<MechanicController> logger,
             IUnitOfBisnes UnitOfBisnes
            )
        {
            _logger = logger;
            _UnitOfBisnes = UnitOfBisnes;
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<MechanicPublicResponse>>> GetAllAsync()
        {
            try
            {
                var results = await _UnitOfBisnes._MechanicService.GetAllAsync();


                _logger.LogInformation($"Отримали всі івенти з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //GET: api/jobs/Id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<MechanicResponse>> GetByIdDetailedAsync(int id)
        {
            try
            {
                var result = await _UnitOfBisnes._MechanicService.GetByIdDetailedAsync(id);
                if (result == null)
                {
                    _logger.LogInformation($"Механік із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали механіка з бази даних!");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetByIdDetailedAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
