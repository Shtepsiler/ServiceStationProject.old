using Application.DTOs.Respponces;
using Application.Operations.Clients.Commands;
using Application.Operations.Clients.Queries;
using Application.Operations.Jobs.Commands;
using Application.Operations.Jobs.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMemoryCache MemoryCache;

        private readonly IMediator Mediator;

        public ClientsController(IMediator mediator, IMemoryCache memoryCache)
        {
            Mediator = mediator;
            this.MemoryCache = memoryCache;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAllAsync()
        {
            try
            {


                var cacheKey = "ClientList";
                if (!MemoryCache.TryGetValue(cacheKey, out List<ClientDTO> clientList))
                {
                    clientList = (List<ClientDTO>)await Mediator.Send(new GetClientsQuery());

                    MemoryCache.Set(cacheKey, clientList);
                }

                return Ok(clientList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteClientCommand { Id = id });
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
