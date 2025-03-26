namespace TravelBookingSystem.Search.Core.Interfaces;

public interface ISearchRequestService
{
    public string StartSearch(string from, string to, DateTime date, string connectionId);
    public bool? GetStatus(string searchId);
}
