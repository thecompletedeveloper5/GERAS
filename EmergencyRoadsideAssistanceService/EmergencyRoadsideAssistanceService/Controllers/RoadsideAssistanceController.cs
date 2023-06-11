using EmergencyRoadsideAssistanceService.DTOs;
using EmergencyRoadsideAssistanceService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Net;

namespace EmergencyRoadsideAssistanceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoadsideAssistanceController : ControllerBase
    {
        private readonly ILogger<RoadsideAssistanceController> _logger;
        private readonly IRoadsideAssistanceService _roadsideAssistanceService;

        private static readonly string message = "An error occurred while processing the request.";

        public RoadsideAssistanceController(ILogger<RoadsideAssistanceController> logger, IRoadsideAssistanceService roadsideAssistanceService)
        {
            _logger = logger;
            _roadsideAssistanceService = roadsideAssistanceService;
        }

        [HttpGet("FindNearestAssistants")]
        [ProducesResponseType(typeof(SortedSet<Assistant>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> FindNearestAssistants([FromQuery] double? latitude, [FromQuery] double? longitude, [FromQuery] int limit = 5)
        {
            try
            {
                if (latitude == null || longitude == null)
                {
                    return BadRequest(new ErrorDetails { ErrorText = "Invalid location provided !" });
                }

                if (limit < 1)
                {
                    return BadRequest(new ErrorDetails { ErrorText = "Limit cannot be less than 1 !" });
                }

                var currentLocation = new Geolocation { Latitude = latitude.Value, Longitude = longitude.Value };
                var assistants = await _roadsideAssistanceService.FindNearestAssistants(currentLocation, limit);
                return Ok(assistants);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{message} MethodName: {nameof(FindNearestAssistants)}", latitude, longitude, limit);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new ErrorDetails { ErrorText = message });
            }

            //return BadRequest();

        }

        [HttpPost("UpdateAssistantLocation")]
        [ProducesResponseType(typeof(NonErrorDetails), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateAssistantLocation(AssistantGeolocationDto assistantGeolocationDto)
        {
            try
            {

                if (assistantGeolocationDto == null)
                {
                    return BadRequest(new ErrorDetails { ErrorText = "Input not provided !" });
                }

                if (assistantGeolocationDto.Assistant == null || assistantGeolocationDto.Assistant.AssistantId == 0)
                {
                    return BadRequest(new ErrorDetails { ErrorText = "Invalid assistant provided !" });
                }

                if (assistantGeolocationDto.Geolocation == null)
                {
                    return BadRequest(new ErrorDetails { ErrorText = "Invalid location provided !" });
                }

                await _roadsideAssistanceService.UpdateAssistantLocation(assistantGeolocationDto.Assistant, assistantGeolocationDto.Geolocation);
                return Ok(new NonErrorDetails { ResponseMessage = "Successfully updated assistant location. " });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{message} MethodName: {nameof(UpdateAssistantLocation)}", assistantGeolocationDto);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new ErrorDetails { ErrorText = message });
            }
        }

        [HttpPost("ReserveAssistant")]
        [ProducesResponseType(typeof(NonErrorDetails), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ReserveAssistant(CustomerGeolocationDto customerGeolocationDto)
        {
            try
            {
                if (customerGeolocationDto == null)
                {
                    return BadRequest(new ErrorDetails { ErrorText = "Invalid inputs provided !" });
                }

                if (customerGeolocationDto.Geolocation == null)
                {
                    return BadRequest(new ErrorDetails { ErrorText = "Invalid location provided !" });
                }

                if (customerGeolocationDto.Customer == null || customerGeolocationDto.Customer.CustomerId == 0)
                {
                    return BadRequest(new ErrorDetails { ErrorText = "Invalid customer information provided !" });
                }

                var assistant = await _roadsideAssistanceService.ReserveAssistant(customerGeolocationDto.Customer, customerGeolocationDto.Geolocation);

                if (assistant.Any())
                {
                    return Ok(new NonErrorDetails { ResponseMessage = $"{assistant.First().AssistantName} has been reserved." });
                }
                else
                {
                    return Ok(new NonErrorDetails { ResponseMessage = "No assistants available at the moment. Please call customer service" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{message} MethodName: {nameof(ReserveAssistant)}", customerGeolocationDto);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new ErrorDetails { ErrorText = message });
            }
        }

        [HttpPost("ReleaseAssistant")]
        [ProducesResponseType(typeof(NonErrorDetails), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ReleaseAssistant(CustomerAssistantDto customerAssistantDto)
        {
            try
            {

                if (customerAssistantDto == null)
                {
                    return BadRequest(new ErrorDetails { ErrorText = "Invalid inputs provided !" });
                }

                if (customerAssistantDto.Customer == null || customerAssistantDto.Customer.CustomerId == 0)
                {
                    return BadRequest(new ErrorDetails { ErrorText = "Invalid customer information provided !" });
                }

                if (customerAssistantDto.Assistant == null || customerAssistantDto.Assistant.AssistantId == 0)
                {
                    return BadRequest(new ErrorDetails { ErrorText = "Invalid assistant provided !" });
                }

                await _roadsideAssistanceService.ReleaseAssistant(customerAssistantDto.Customer, customerAssistantDto.Assistant);
                return Ok(new NonErrorDetails { ResponseMessage = "Assistant successfully released. " });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{message} MethodName: {nameof(ReleaseAssistant)}", customerAssistantDto);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new ErrorDetails { ErrorText = message });
            }
        }

    }
}
