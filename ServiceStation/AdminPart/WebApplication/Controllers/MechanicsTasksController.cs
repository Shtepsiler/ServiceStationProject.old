using Application.DTOs.Respponces;
using Application.Operations.Jobs.Queries;
using Application.Operations.Mechanics.Queries;
using Application.Operations.MechanicsTasks.Commands;
using Application.Operations.MechanicsTasks.Queries;
using Application.Operations.Models.Commands;
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
    public class MechanicsTasksController : ControllerBase
    {
        public IMediator Mediator { get; }
        private readonly IMemoryCache MemoryCache;


        public MechanicsTasksController(IMediator mediator, IMemoryCache memoryCache)
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
                await Mediator.Send(new DeleteMechanicTaskCommand() { Id = id });
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
        public async Task<IActionResult> Create(CreateMechanicTaskCommand comand)
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
        public async Task<ActionResult<IEnumerable<MechanicsTasksDTO>>> GetAllAsync()
        {
            try
            {
                var cacheKey = "MechanicTaskList";
                if (!MemoryCache.TryGetValue(cacheKey, out List<MechanicsTasksDTO> MechanicTaskList))
                {
                    MechanicTaskList = (List<MechanicsTasksDTO>)await Mediator.Send(new GetMechanicsTasksQuery());

                    MemoryCache.Set(cacheKey, MechanicTaskList);
                }

                return Ok(MechanicTaskList);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MechanicsTasksDTO>> GetByIdAsync(int id)
        {
            try
            {
                var results = await Mediator.Send(new GetMechanicTaskByIdQuery() { Id = id });


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
        public async Task<IActionResult> Update(UpdateMechanicTaskCommand comand)
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
