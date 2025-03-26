using Microsoft.AspNetCore.SignalR;
using TravelBookingSystem.Search.Core.Interfaces;
using TravelBookingSystem.Search.API.Hubs;

namespace TravelBookingSystem.Search.API.Services;

public class SignalRSearchNotificationService(IHubContext<SearchHub> hubContext) : ISearchNotificationService
{
    public async Task NotifySearchCompletedAsync(string connectionId, string searchId)
    {
        await hubContext.Clients
            .Client(connectionId)
            .SendAsync("SearchCompleted", searchId);
    }
}