namespace Clean.Architecture.Inventory.Application.Interfaces
{
    public interface ILoggingService
    {
        Task LogErrorAsync(string message, string stackTrace);
    }
}
