using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;
using Saga.Events;

namespace Saga.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IBus _bus;

        public OrderController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost(Name = "StartOrderSaga")]
        public async Task<IActionResult> PostAsync()
        {
            await _bus.Send(new OrderCreatedEvent { OrderID = Guid.NewGuid() });
            return Ok();
        }
    }
}