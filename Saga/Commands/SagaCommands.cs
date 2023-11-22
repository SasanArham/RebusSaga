namespace Saga.Commands
{
    public record StartStepOneCommand
    {
        public Guid OrderID { get; set; }
    }

    public record StartStepTwoCommand
    {
        public Guid OrderID { get; set; }
    }

    public record StartStepThreeCommand
    {
        public Guid OrderID { get; set; }
    }
}
