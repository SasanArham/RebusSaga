namespace Saga.Events
{
    public record StepOneProcessedEvent
    {
        public Guid OrderID { get; set; }
    }
}
