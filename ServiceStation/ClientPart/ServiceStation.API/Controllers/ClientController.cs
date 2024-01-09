using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ServiceStation.BLL.DTO.Requests;
using ServiceStation.BLL.DTO.Responses;
using ServiceStation.BLL.Services.Interfaces;
using ServiceStation.DAL.Entities;
using ServiceStation.DAL.Repositories.Contracts;

namespace ServiceStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IDistributedCache distributedCache;

        private IUnitOfWork _UnitOfWork;
        private IUnitOfBisnes _UnitOfBisnes;
        private IMapper _mapper;
        private readonly ILogger<ClientController> _logger;
        public ClientController(
            ILogger<ClientController> logger,
             IUnitOfWork UnitOfWork,
             IUnitOfBisnes UnitOfBisnes,
             IMapper mapper
,
             IDistributedCache distributedCache)
        {
            _logger = logger;
            _UnitOfWork = UnitOfWork;
            _UnitOfBisnes = UnitOfBisnes;
            _mapper = mapper;
            this.distributedCache = distributedCache;
        }



        [HttpPost("refreshToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JwtResponse>> RefreshToken(
[FromHeader] string RefreshToken)
        {
            try
            {
                if (RefreshToken == null) { throw new ArgumentNullException(nameof(RefreshToken)); }

                var response = await _UnitOfBisnes._ClientService.RenewAccesToken(RefreshToken);
                if (response == null) return StatusCode(StatusCodes.Status500InternalServerError);

                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { e.Message });
            }
        }

        //GET: api/jobs/Id
        /*  [Authorize]*/
       
        [HttpGet("{name}")]
        public async Task<ActionResult<ClientResponse>> GetByNameAsync(string name)
        {
            try
            {
                var result = _mapper.Map<Client, ClientResponse>(await _UnitOfWork._ClientManager.FindByNameAsync(name));

                if (result == null)
                {
                    _logger.LogInformation($"Івент із name: {name}, не був знайдейний у базі даних");
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




        //POST: api/jobs/Id
        [Authorize]
        [HttpPut("{name}")]
        public async Task<ActionResult> UpdateAsync(string name, [FromBody] ClientRequest client)
        {
            try
            {
                if (client == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт  є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт  є некоректним");
                }
                if (name != client.ClientName)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт  є некоректним");
                }

                await _UnitOfBisnes._ClientService.UpdateAsync(name, client);
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
        [HttpDelete("{name}")]
        public async Task<ActionResult> DeleteByNameAsync(string name)
        {
            try
            {
                var client = await _UnitOfBisnes._ClientService.GetClientByName(name);
                if (client == null)
                {
                    _logger.LogInformation($"Запис із Name: {name}, не був знайдейний у базі даних");
                    return NotFound();
                }

                await _UnitOfBisnes._ClientService.DeleteAsync(name);
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
