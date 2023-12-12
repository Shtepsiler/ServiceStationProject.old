using Application.DTOs.Respponces;
using Application.Operations.Jobs.Queries;
using Application.Operations.Models.Commands;
using Application.Operations.Orders.Queries;
using Application.Operations.Parts.Commands;
using Application.Operations.Parts.Queries;
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
    public class PartController : ControllerBase
    {
        public IMediator Mediator { get; }
        private readonly IMemoryCache MemoryCache;

        public PartController(IMediator mediator, IMemoryCache memoryCache)
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
                await Mediator.Send(new DeletePartCommand() { Id = id });
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
        public async Task<IActionResult> Create(CreatePartCommand comand)
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
        public async Task<ActionResult<IEnumerable<PartDTO>>> GetAllAsync()
        {
            try
            {
                var cacheKey = "PartList";
                if (!MemoryCache.TryGetValue(cacheKey, out List<PartDTO> PartList))
                {
                    PartList = (List<PartDTO>)await Mediator.Send(new GetPartsQuery());

                    MemoryCache.Set(cacheKey, PartList);
                }

                return Ok(PartList);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PartDTO>> GetByIdAsync(int id)
        {
            try
            {
                var results = await Mediator.Send(new GetPartByIdQuery() { Id = id });


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
        public async Task<ActionResult> Update(UpdatePartCommand comand)
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
