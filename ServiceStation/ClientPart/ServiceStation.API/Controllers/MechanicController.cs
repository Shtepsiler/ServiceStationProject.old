using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ServiceStation.BLL.DTO.Responses;
using ServiceStation.BLL.Services.Interfaces;
using System.Text;

namespace ServiceStation.API.Controllers
{
    //видалити
    [Route("api/[controller]")]
    [ApiController]
    public class MechanicController : ControllerBase
    {



        private IUnitOfBisnes _UnitOfBisnes;
        private readonly IDistributedCache distributedCache;

        private readonly ILogger<MechanicController> _logger;
        public MechanicController(
            ILogger<MechanicController> logger,
             IUnitOfBisnes UnitOfBisnes
,
             IDistributedCache distributedCache)
        {
            _logger = logger;
            _UnitOfBisnes = UnitOfBisnes;
            this.distributedCache = distributedCache;
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<MechanicPublicResponse>>> GetAllAsync()
        {
            try
            {
                var cacheKey = "mechanicList";
                string serializedMechanicList;
                var mechanicList = new List<MechanicPublicResponse>();
                var redisMechanicList = await distributedCache.GetAsync(cacheKey);
                if (redisMechanicList != null)
                {
                    serializedMechanicList = Encoding.UTF8.GetString(redisMechanicList);
                    mechanicList = JsonConvert.DeserializeObject<List<MechanicPublicResponse>>(serializedMechanicList);
                }
                else
                {
                    mechanicList = (List<MechanicPublicResponse>)await _UnitOfBisnes._MechanicService.GetAllAsync();
                    serializedMechanicList = JsonConvert.SerializeObject(mechanicList);
                    redisMechanicList = Encoding.UTF8.GetBytes(serializedMechanicList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(1));
                    await distributedCache.SetAsync(cacheKey, redisMechanicList, options);
                }
                _logger.LogInformation($"MechanicController            GetAllAsync");
                return Ok(mechanicList);

            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
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
                    _logger.LogInformation($"MechanicController            GetByIdDetailedAsync");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
