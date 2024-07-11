﻿using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestRegisterTripJson request)
        {
            try
            {
                var useCase = new RegisterTripUseCase();
                var response = useCase.Execute(request);

                return Created(string.Empty, response);
            }
            catch (JourneyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch 
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, ResourceErrorMessages.UNKNOWN_ERROR);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var useCase = new GetAllTripsUseCase();
            var result = useCase.Execute();

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public IActionResult GetById([FromRoute]Guid id)
        {
            try
            {
                var useCase = new GetTripByIdUseCase();
                var response = useCase.Execute(id);

                return Ok(response);
            }
            catch (JourneyException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResourceErrorMessages.UNKNOWN_ERROR);
            }
        }
    }
}
