using DemoApp.Business.Services;
using DemoApp.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DemoApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingsService _bookingsService;

        public BookingsController(IBookingsService bookingsService)
        {
            _bookingsService = bookingsService ?? throw new ArgumentNullException(nameof(bookingsService));
        }

        [HttpPost]
        [EndpointSummary("Endpoint for creating a booking.")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("/book")]
        public async Task<IActionResult> AddBooking(BookingRequest request)
        {
            var bookingId = await _bookingsService.AddBooking(request);
            return Created(nameof(AddBooking), new { BookingRef = bookingId });
        }

        [HttpPost]
        [EndpointSummary("Endpoint for cancelling a booking.")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("/cancel")]
        public async Task<IActionResult> CancelBooking(CancellationRequest request)
        {
            await _bookingsService.CancelBooking(request);
            return Ok();
        }

        [HttpGet]
        [EndpointSummary("Endpoint for getting all booking.")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _bookingsService.GetBookings();
            return Ok(bookings);
        }
    }
}
