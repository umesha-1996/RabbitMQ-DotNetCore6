using FormularAirline.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormularAirline.API.Controllers
{
    [ApiController]
    [Route("controller")]
    public class BookingController : Controller
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IMessageProducer _messageProducer;

        //In-Memory
        public static readonly List<Booking> _bookings = new List<Booking>();

        public BookingController(ILogger<BookingController> logger, IMessageProducer messageProducer)
        {
            _logger = logger;
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public IActionResult CratingBooking(Booking newBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _bookings.Add(newBooking);

            _messageProducer.SendingMessages<Booking>(newBooking);

            return Ok();
        }
    }
}
