using Rebus.Bus;
using Rebus.Handlers;
using Saga.Events;
using Serilog;

namespace Saga.Commands
{
    public class StartStepThreeCommandHandler : IHandleMessages<StartStepThreeCommand>
    {
        private readonly IBus _bus;

        public StartStepThreeCommandHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(StartStepThreeCommand message)
        {
            Log.Information("Handling StartStepThreeCommand");
            await _bus.Send(new StepThreeProcessedEvent { OrderID = message.OrderID });
        }
    }
}
