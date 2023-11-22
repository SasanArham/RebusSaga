using Rebus.Bus;
using Rebus.Handlers;
using Saga.Events;
using Serilog;

namespace Saga.Commands
{
    public class StartStepOneCommandHandler : IHandleMessages<StartStepOneCommand>
    {
        private readonly IBus _bus;

        public StartStepOneCommandHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(StartStepOneCommand message)
        {
            Log.Information("Handling StartStepOneCommand");
            await _bus.Send(new StepOneProcessedEvent { OrderID = message.OrderID });
        }
    }
}
