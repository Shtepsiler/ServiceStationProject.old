using Application.DTOs.Respponces;
using Application.Operations.Jobs.Queries;
using Application.Operations.Models.Commands;
using Application.Operations.Orders.Queries;
using Application.Operations.PartsNeeded.Commands;
using Application.Operations.PartsNeeded.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.CompilerServices;

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PartNeededController : ControllerBase
    {
        public IMediator Mediator { get; }
        private readonly IMemoryCache MemoryCache;

        public PartNeededController(IMediator mediator, IMemoryCache memoryCache)
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
                await Mediator.Send(new DeletePartNeededCommand() { Id = id });
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
        public async Task<IActionResult> Create(CreatePartsNeededCommand comand)
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
        //GET: api/jobs
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PartNeededDTO>>> GetAllAsync()
        {
            try
            {
                var cacheKey = "PartNeededList";
                if (!MemoryCache.TryGetValue(cacheKey, out List<PartNeededDTO> PartNeededList))
                {
                    PartNeededList = (List<PartNeededDTO>)await Mediator.Send(new GetPartsNeededQuery());

                    MemoryCache.Set(cacheKey, PartNeededList);
                }

                return Ok(PartNeededList);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }        //GET: api/jobs
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PartNeededDTO>> GetByIdAsync(int id)
        {
            try
            {
                var results = await Mediator.Send(new GetPartNeededByIdQuery() { Id = id });


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
        public async Task<ActionResult> Update(UpdatePartneededCommand comand)
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
