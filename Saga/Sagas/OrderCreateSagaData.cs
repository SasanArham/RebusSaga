using Rebus.Sagas;

namespace Saga.Sagas
{
    public class OrderCreateSagaData : ISagaData
    {
        public Guid Id { get; set; }
        public int Revision { get; set; }
        public string State { get; set; }

    }
}
