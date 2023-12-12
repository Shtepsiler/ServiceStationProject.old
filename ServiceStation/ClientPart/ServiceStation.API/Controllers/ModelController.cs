using Microsoft.AspNetCore.Mvc;
using ServiceStation.DAL.Entities;

using ServiceStation.BLL.Services.Interfaces;
using ServiceStation.BLL.DTO.Responses;
using ServiceStation.BLL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using StackExchange.Redis;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using DocumentFormat.OpenXml.InkML;
using System.Text;
using Newtonsoft.Json;

namespace ServiceStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        //видалити

        private IUnitOfBisnes _UnitOfBisnes;
        private readonly IDistributedCache distributedCache;

        private readonly ILogger<ModelController> _logger;
        public ModelController(
            ILogger<ModelController> logger,
             IUnitOfBisnes UnitOfBisnes
,
             IDistributedCache redis)
        {
            _logger = logger;
            _UnitOfBisnes = UnitOfBisnes;
            distributedCache = redis;
        }

        //GET: api/jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelResponse>>> GetAllAsync()
        {
            try
            {
                var cacheKey = "modelList";
                string serializedModelList;
                var modelList = new List<ModelResponse>();
                var redisModelList = await distributedCache.GetAsync(cacheKey);
                if (redisModelList != null)
                {
                    serializedModelList = Encoding.UTF8.GetString(redisModelList);
                    modelList = JsonConvert.DeserializeObject<List<ModelResponse>>(serializedModelList);
                }
                else
                {
                    modelList = (List<ModelResponse>) await _UnitOfBisnes._ModelService.GetAllAsync();
                    serializedModelList = JsonConvert.SerializeObject(modelList);
                    redisModelList = Encoding.UTF8.GetBytes(serializedModelList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await distributedCache.SetAsync(cacheKey, redisModelList, options);
                }
                _logger.LogInformation($"Отримали всі івенти з бази даних!");
                return Ok(modelList);



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

        public async Task<ActionResult<ModelResponse>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _UnitOfBisnes._ModelService.GetByIdAsync(id);
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
        [HttpPost]
        [Authorize]

        public async Task<ActionResult> PostAsync([FromBody] ModelRequest manager)
        {
            try
            {
                if (manager == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт івенту є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт івенту є некоректним");
                }
                await _UnitOfBisnes._ModelService.PostAsync(manager);
  
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі PostAsync - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //POST: api/jobs/id
        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult> UpdateAsync(int id, [FromBody] ModelRequest manager)
        {
            try
            {
                if (manager == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт івенту є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт івенту є некоректним");
                }
                manager.Id = id;

                await _UnitOfBisnes._ModelService.UpdateAsync(id, manager);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі PostAsync - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //GET: api/jobs/Id
        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult> DeleteByIdAsync(int id)
        {
            try
            {
                var event_entity = await _UnitOfBisnes._ModelService.GetByIdAsync(id);
                if (event_entity == null)
                {
                    _logger.LogInformation($"Запис із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }

                await _UnitOfBisnes._ModelService.DeleteByIdAsync(id);
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
