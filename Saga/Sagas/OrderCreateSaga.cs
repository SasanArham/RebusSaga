using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;
using Saga.Commands;
using Saga.Events;
using Serilog;

namespace Saga.Sagas
{
    public class OrderCreateSaga : Saga<OrderCreateSagaData>
        , IAmInitiatedBy<OrderCreatedEvent>
        , IHandleMessages<StepOneProcessedEvent>
        , IHandleMessages<StepTwoProcessedEvent>
        , IHandleMessages<StepThreeProcessedEvent>
    {
        private readonly IBus _bus;

        public OrderCreateSaga(IBus bus)
        {
            _bus = bus;
        }

        protected override void CorrelateMessages(ICorrelationConfig<OrderCreateSagaData> config)
        {
            config.Correlate<OrderCreatedEvent>(message => message.OrderID, saga => saga.Id);
            config.Correlate<StepOneProcessedEvent>(message => message.OrderID, saga => saga.Id);
            config.Correlate<StepTwoProcessedEvent>(message => message.OrderID, saga => saga.Id);
            config.Correlate<StepThreeProcessedEvent>(message => message.OrderID, saga => saga.Id);
        }

        public async Task Handle(OrderCreatedEvent message)
        {
            if (!IsNew)
            {
                return;
            }

            await _bus.Send(new StartStepOneCommand { OrderID = Data.Id });
        }

        public async Task Handle(StepOneProcessedEvent message)
        {
            Log.Information("Step one processed");
            Data.State = "Step one processed";
            await _bus.Send(new StartStepTwoCommand { OrderID = Data.Id });
        }

        public async Task Handle(StepTwoProcessedEvent message)
        {
            Log.Information("Step two processed");
            Data.State = "Step two processed";
            await _bus.Send(new StartStepThreeCommand { OrderID = Data.Id });
        }

        public Task Handle(StepThreeProcessedEvent message)
        {
            Data.State = "Step three processed";
            Log.Information("Step three processed");
            MarkAsComplete();
            return Task.CompletedTask;
        }
    }
}
