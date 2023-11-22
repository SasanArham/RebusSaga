namespace Saga.Events
{
    public record StepTwoProcessedEvent
    {
        public Guid OrderID { get; set; }
        public string StepTwoData { get; set; }
    }
}
