using TravelBookingSystem.Search.API.Models;
using TravelBookingSystem.Search.Core.Jobs;

namespace TravelBookingSystem.Search.API.Extensions;

public static class MappingExtensions
{
    public static SearchJob MapToSearchJob(this SearchRequest request, string searchId, string connectionId)
    {
        return new SearchJob
        {
            SearchId = searchId,
            ConnectionId = connectionId,
            From = request.From,
            To = request.To,
            Date = request.Date
        };
    }
}
