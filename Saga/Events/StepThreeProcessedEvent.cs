namespace Saga.Events
{
    public record StepThreeProcessedEvent
    {
        public Guid OrderID { get; set; }
        public List<int> StepThreeDatas { get; set; }
    }
}
