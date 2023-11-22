namespace Saga.Events
{
    public record OrderCreatedEvent
    {
        public Guid OrderID { get; set; }
    }
}
