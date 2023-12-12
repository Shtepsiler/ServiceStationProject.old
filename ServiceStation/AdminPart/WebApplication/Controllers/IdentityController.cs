using Application.DTOs.Respponces;
using Application.Operations.Managers.Commands;
using Domain.Exeptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        public IdentityController(IMediator mediator)
        {
            Mediator = mediator;
        }

        public IMediator Mediator { get; }




        [HttpPost("signIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JwtResponse>> SignInAsync(
            [FromBody] SingInCommand request)
        {
            try
            {


                if (request == null) { throw new ArgumentNullException(nameof(request)); }


                var response = await Mediator.Send(request);
                return Ok(response);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }



        [HttpPost("signUp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JwtResponse>> SignUpAsync(
         [FromBody] SingUpCommand request)
        {
            try
            {
                if (request == null) { throw new ArgumentNullException(nameof(request)); }


                var response = await Mediator.Send(request);

                return Ok(response);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { e.Message });
            }
        }










    }
}
