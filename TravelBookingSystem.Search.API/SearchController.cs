using Microsoft.AspNetCore.Mvc;
using TravelBookingSystem.Search.API.Models;
using TravelBookingSystem.Search.Core.Interfaces;

namespace TravelBookingSystem.Search.API;

[ApiController]
[Route("search")]
public class SearchController(ISearchRequestService searchService) : ControllerBase
{
    [HttpPost]
    public IActionResult StartSearch([FromBody] SearchRequest request, [FromQuery] string connectionId)
    {
        var searchId = searchService.StartSearch(
            request.From,
            request.To,
            request.Date,
            connectionId);

        return Ok(new { searchId });
    }

    [HttpGet("status/{searchId}")]
    public IActionResult CheckStatus(string searchId)
    {
        var status = searchService.GetStatus(searchId);
        if (status == null)
            return NotFound();

        return Ok(new { isReady = status });
    }
}