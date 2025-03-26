using System.Threading.Channels;
using Microsoft.Extensions.Caching.Memory;
using TravelBookingSystem.Search.Core.Interfaces;
using TravelBookingSystem.Search.Core.Jobs;

namespace TravelBookingSystem.Search.Application.Services;

public class SearchRequestService : ISearchRequestService
{
    private readonly Channel<SearchJob> _queue;
    private readonly IMemoryCache _cache;

    public SearchRequestService(Channel<SearchJob> queue, IMemoryCache cache)
    {
        _queue = queue;
        _cache = cache;
    }

    public string StartSearch(string from, string to, DateTime date, string connectionId)
    {
        var searchId = Guid.NewGuid().ToString();

        _cache.Set(searchId, false, TimeSpan.FromMinutes(10));

        var job = new SearchJob
        {
            SearchId = searchId,
            From = from,
            To = to,
            Date = date,
            ConnectionId = connectionId
        };

        _queue.Writer.TryWrite(job);

        return searchId;
    }

    public bool? GetStatus(string searchId)
    {
        if (_cache.TryGetValue(searchId, out bool isReady))
        {
            return isReady;
        }

        return null;
    }
}