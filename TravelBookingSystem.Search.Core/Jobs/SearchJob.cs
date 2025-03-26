namespace TravelBookingSystem.Search.Core.Jobs;

public class SearchJob
{
    public string SearchId { get; set; }
    public string ConnectionId { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public DateTime Date { get; set; }
}
