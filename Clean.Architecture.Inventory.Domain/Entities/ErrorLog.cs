namespace Clean.Architecture.Inventory.Domain.Entities
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime OccurredAt { get; set; }
    }
}
