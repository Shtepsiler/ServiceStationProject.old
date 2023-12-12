using Application.DTOs.Respponces;
using Application.Operations.Jobs.Queries;
using Application.Operations.Models.Commands;
using Application.Operations.Orders.Queries;
using Application.Operations.Vendors.Commands;
using Application.Operations.Vendors.Queries;
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
    public class VendorsController : ControllerBase
    {
        public IMediator Mediator { get; }
        private readonly IMemoryCache MemoryCache;


        public VendorsController(IMediator mediator, IMemoryCache memoryCache)
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
                await Mediator.Send(new DeleteVendorCommand() { Id = id });
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
        public async Task<IActionResult> Create(CreateVendorCommand comand)
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
        public async Task<ActionResult<IEnumerable<VendorDTO>>> GetAllAsync()
        {
            try
            {
                var cacheKey = "VendorList";
                if (!MemoryCache.TryGetValue(cacheKey, out List<OrderDTO> VendorList))
                {
                    VendorList = (List<OrderDTO>)await Mediator.Send(new GetVendorsQuery());

                    MemoryCache.Set(cacheKey, VendorList);
                }

                return Ok(VendorList);


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }   

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VendorDTO>> GetByIdAsync(int id)
        {
            try
            {
                var results = await Mediator.Send(new GetVendorByIdQuery() { Id = id });


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
        public async Task<ActionResult> Update(UpdateVendorCommand comand)
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
