using Microsoft.AspNetCore.Mvc;
using FluentValidation;

using Microsoft.AspNetCore.Authorization;
using System.Data;
using MediatR;
using Application.Operations.Jobs.Commands;
using Application.DTOs.Respponces;
using Application.Operations.Jobs.Queries;
using Application.Operations.Managers.Commands;
using Application.Common.Validation;
using Microsoft.Extensions.Caching.Memory;
using Application.Operations.Clients.Queries;

namespace ServiceStation.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private IMediator Mediator;
        private UpdateJobCommandValidator UdateJobCommandValidator;
        private CreateJobCommandValidator CreateJobCommandValidator;
        private readonly IMemoryCache MemoryCache;

        public JobController(IMediator mediator, UpdateJobCommandValidator updateJobCommandValidator, CreateJobCommandValidator createJobCommandValidator, IMemoryCache memoryCache)
        {
            Mediator = mediator;
            UdateJobCommandValidator = updateJobCommandValidator;
            CreateJobCommandValidator = createJobCommandValidator;
            MemoryCache = memoryCache;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteJobCommand { Id = id });
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateJobCommand comand)
        {
            try
            {
                var isValid = CreateJobCommandValidator.Validate(comand);
                if (isValid.IsValid)
                {
                    await Mediator.Send(comand);
                    return Ok();
                }
                else
                {
                    return ValidationProblem(isValid.Errors.ToString());
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<JobDTO>>> GetAllAsync()
        {
            try
            {

                var cacheKey = "JobList";
                if (!MemoryCache.TryGetValue(cacheKey, out List<JobDTO> jobList))
                {
                    jobList = (List<JobDTO>)await Mediator.Send(new GetJobsQuery());

                    MemoryCache.Set(cacheKey, jobList);
                }


                return Ok(jobList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JobDTO>> GetByIdAsync(int id)
        {
            try
            {
                var results = await Mediator.Send(new GetJobByIdQuery() { Id = id });


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
        public async Task<ActionResult> Update(UpdateJobCommand comand)
        {
            try
            {
                var isValid = UdateJobCommandValidator.Validate(comand);
                if (isValid.IsValid)
                {
                    await Mediator.Send(comand);
                    return Ok();
                }
                else
                {
                    return ValidationProblem(isValid.Errors.ToString());
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

    }
}
