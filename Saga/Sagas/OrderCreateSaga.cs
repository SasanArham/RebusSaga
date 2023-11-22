using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;
using Saga.Commands;
using Saga.Events;

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
            await _bus.Send(new StartStepTwoCommand { OrderID = Data.Id });
        }

        public async Task Handle(StepTwoProcessedEvent message)
        {
            await _bus.Send(new StartStepThreeCommand { OrderID = Data.Id });
        }

        public async Task Handle(StepThreeProcessedEvent message)
        {
            throw new NotImplementedException();
        }
    }
}
