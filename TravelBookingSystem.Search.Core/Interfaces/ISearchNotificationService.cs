namespace TravelBookingSystem.Search.Core.Interfaces;

public interface ISearchNotificationService
{
    Task NotifySearchCompletedAsync(string connectionId, string searchId);
}