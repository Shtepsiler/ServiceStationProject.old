using Application.DTOs.Respponces;
using Application.Operations.Jobs.Queries;
using Application.Operations.Mechanics.Queries;
using Application.Operations.Models.Commands;
using Application.Operations.Models.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.CompilerServices;
using WebApplication.MessageBroker.EventBus;

namespace WebApplication.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
       
        private IMediator Mediator { get; }
        private readonly IMemoryCache MemoryCache;

        public ModelController(IMediator mediator, IMemoryCache memoryCache)
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

                await Mediator.Send(new DeleteModelCommand() { Id = id });
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
        public async Task<IActionResult> Create(CreateModelCommand comand)
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
        public async Task<ActionResult<IEnumerable<ModelDTO>>> GetAllAsync()
        {
            try
            {
                var cacheKey = "ModelList";
                if (!MemoryCache.TryGetValue(cacheKey, out List<ModelDTO> ModelList))
                {
                    ModelList = (List<ModelDTO>)await Mediator.Send(new GetModelsQuery());

                    MemoryCache.Set(cacheKey, ModelList);
                }

                return Ok(ModelList);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ModelDTO>> GetByIdAsync(int id)
        {
            try
            {
                var results = await Mediator.Send(new GetModelByIdQuery() { Id = id });


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
        public async Task<ActionResult> Update(UpdateModelCommand comand)
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
