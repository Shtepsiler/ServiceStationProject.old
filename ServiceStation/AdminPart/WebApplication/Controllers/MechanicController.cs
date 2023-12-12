using Application.DTOs.Respponces;
using Application.Operations.Jobs.Queries;
using Application.Operations.Mechanics.Commands;
using Application.Operations.Mechanics.Queries;
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
    public class MechanicController : ControllerBase
    {
        public IMediator Mediator { get; }
        private readonly IMemoryCache MemoryCache;


        public MechanicController(IMediator mediator, IMemoryCache memoryCache)
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
                await Mediator.Send(new DeleteMechanicCommand() { Id = id });
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
        public async Task<IActionResult> Create(CreateMechanicCommand comand)
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
        public async Task<ActionResult<IEnumerable<MechanicDTO>>> GetAllAsync()
        {
            try
            {

                var cacheKey = "MechanicList";
                if (!MemoryCache.TryGetValue(cacheKey, out List<MechanicDTO> MechanicList))
                {
                    MechanicList = (List<MechanicDTO>)await Mediator.Send(new GetMechanicsQuery());

                    MemoryCache.Set(cacheKey, MechanicList);
                }

                return Ok(MechanicList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MechanicDTO>> GetByIdAsync(int id)
        {
            try
            {
                var results = await Mediator.Send(new GetMechaincByIdQuery() { Id = id });


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
        public async Task<ActionResult> Update(UpdateMechanicCommand comand)
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
