using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeatReservation.BusinessLogic.DTO;
using SeatReservation.BusinessLogic.Interfaces;

namespace SeatReservation.Controllers.Api
{
    [Route("api/Seats")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatList _seatList;
        private readonly ISeatAdd _seatAdd;
        private readonly IEmailProvider _emailProvider;
        private readonly ILogger<SeatsController> _logger;

        public SeatsController(ISeatList seatList, ISeatAdd seatAdd, IEmailProvider emailProvider, ILogger<SeatsController> logger)
        {
            _seatList = seatList;
            _seatAdd = seatAdd;
            _emailProvider = emailProvider;
            _logger = logger;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _seatList.List(id);

                if (result.Count == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Create(SeatAddDTO bookings)
        {

            if (bookings.Items.Count == 0 || bookings.Items.Count > 4)
            {
                return BadRequest("A minimum of 1 and a maximum of 4 seats can be reserved per transaction");
            }

            try
            {
                var result = _seatAdd.Create(bookings.EventId, bookings.Items);

                foreach (var item in result)
                {
                    _emailProvider.SendConfirmationEmail(item);
                }

                return Created(new Uri(Request.Host + "/" + Request.Path + "/" + bookings.EventId), bookings);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                //this method would receive the identificator send in the email as a link 
                //and be used to remove the booking
                throw new NotImplementedException();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return StatusCode(500);
            }
        }
    }
}