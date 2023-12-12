using Application.DTOs.Respponces;
using Application.Operations.Clients.Queries;
using Application.Operations.Jobs.Queries;
using Application.Operations.Managers.Commands;
using Application.Operations.Managers.Queries;
using Application.Operations.Models.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IMemoryCache MemoryCache;

        public IMediator Mediator { get; }

        public ManagerController(IMediator mediator, IMemoryCache memoryCache)
        {
            Mediator = mediator;
            MemoryCache = memoryCache;
        }
      
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteManagerCommand() { Id = id });
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateManagerCommand comand)
        {
            try
            {
                await Mediator.Send(comand);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ManagerDTO>>> GetAllAsync()
        {
            try
            {

                var cacheKey = "ManagerList";
                if (!MemoryCache.TryGetValue(cacheKey, out List<ClientDTO> managerList))
                {
                    managerList = (List<ClientDTO>)await Mediator.Send(new GetManagersQuery());

                    MemoryCache.Set(cacheKey, managerList);
                }


                return Ok(managerList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ManagerDTO>> GetByIdAsync(int id)
        {
            try
            {
                var results = await Mediator.Send(new GetManagerByIdQuery() { Id = id });


                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(UpdateManagerCommand comand)
        {

            try
            {
                await Mediator.Send(comand);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
