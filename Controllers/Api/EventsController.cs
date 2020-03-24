using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeatReservation.BusinessLogic.Interfaces;

namespace SeatReservation.Controllers.Api
{
    [Route("api/Events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventList _eventList;
        private readonly ILogger<EventsController> _logger;

        public EventsController(IEventList eventList, ILogger<EventsController> logger)
        {
            _eventList = eventList;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _eventList.List();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return StatusCode(500);
            }
        }
    }
}