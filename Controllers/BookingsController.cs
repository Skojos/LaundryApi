using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaundryWebApi.Dtos;
using LaundryWebApi.Extensions;
using LaundryWebApi.Interface;
using LaundryWebApi.Models;
using LaundryWebApi.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LaundryWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api")]
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingsService;

        public BookingsController(IBookingService bookingsService)
        {
            _bookingsService = bookingsService;
        }
       

        [HttpPost("book")]
        public IActionResult BookTime([FromBody]BookingDto dto)
        {
            int authUserId = int.Parse(HttpContext.GetUserId());

            return Created("Sucess", _bookingsService.CreateBooking(dto, authUserId));


        }


        [HttpGet("timeslots")]
        public IEnumerable<TimeSlot> GetAllBookings()
        {
            int authUserId = int.Parse(HttpContext.GetUserId());

            return _bookingsService.GetTimeSlots(authUserId);
        }

     

    }
}
