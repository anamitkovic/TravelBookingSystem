using System.Threading.Channels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TravelBookingSystem.Search.Core.Interfaces;
using TravelBookingSystem.Search.Core.Jobs;

public class SearchWorker : BackgroundService
{
    private readonly Channel<SearchJob> _queue;
    private readonly IMemoryCache _cache;
    private readonly ILogger<SearchWorker> _logger;
    private readonly IServiceProvider _serviceProvider; 

    public SearchWorker(
        Channel<SearchJob> queue,
        IMemoryCache cache,
        IServiceProvider serviceProvider,
        ILogger<SearchWorker> logger)
    {
        _queue = queue;
        _cache = cache;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var job in _queue.Reader.ReadAllAsync(stoppingToken))
        {
          
            using (var scope = _serviceProvider.CreateScope())
            {
                var notifier = scope.ServiceProvider.GetRequiredService<ISearchNotificationService>(); 

                _logger.LogInformation($"Processing job: {job.SearchId}");

                await Task.Delay(10000, stoppingToken); 

                _cache.Set(job.SearchId, true, TimeSpan.FromMinutes(10)); 

                await notifier.NotifySearchCompletedAsync(job.ConnectionId, job.SearchId);

                _logger.LogInformation($"Finished job: {job.SearchId}");
            }
        }
    }
}