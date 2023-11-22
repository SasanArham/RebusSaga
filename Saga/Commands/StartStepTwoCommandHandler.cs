using Rebus.Bus;
using Rebus.Handlers;
using Saga.Events;
using Serilog;

namespace Saga.Commands
{
    public class StartStepTwoCommandHandler : IHandleMessages<StartStepTwoCommand>
    {
        private readonly IBus _bus;

        public StartStepTwoCommandHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(StartStepTwoCommand message)
        {
            Log.Information("Handling StartStepTwoCommand");
            await _bus.Send(new StepTwoProcessedEvent { OrderID = message.OrderID });
        }
    }
}
