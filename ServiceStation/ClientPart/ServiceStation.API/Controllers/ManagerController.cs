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
    public class ManagerController : ControllerBase
    {
        private readonly IDistributedCache distributedCache;

        private IUnitOfBisnes _UnitOfBisnes;

        private readonly ILogger<ManagerController> _logger;
        public ManagerController(
            ILogger<ManagerController> logger,
             IUnitOfBisnes UnitOfBisnes
,
             IDistributedCache distributedCache)
        {
            _logger = logger;
            _UnitOfBisnes = UnitOfBisnes;
            this.distributedCache = distributedCache;
        }

        //GET: api/jobs
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<ManagerResponse>>> GetAllAsync()
        {
            try
            {
                var cacheKey = "managerList";
                string serializedManagerList;
                var ManagerList = new List<ManagerResponse>();
                var redisManagerList = await distributedCache.GetAsync(cacheKey);
                if (redisManagerList != null)
                {
                    serializedManagerList = Encoding.UTF8.GetString(redisManagerList);
                    ManagerList = JsonConvert.DeserializeObject<List<ManagerResponse>>(serializedManagerList);
                }
                else
                {
                    ManagerList = (List<ManagerResponse>)await _UnitOfBisnes._ManagerService.GetAllAsync();
                    serializedManagerList = JsonConvert.SerializeObject(ManagerList);
                    redisManagerList = Encoding.UTF8.GetBytes(serializedManagerList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(1));
                    await distributedCache.SetAsync(cacheKey, redisManagerList, options);
                }
                _logger.LogInformation($"ManagerController            GetAllAsync");
                return Ok(ManagerList);

            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

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
