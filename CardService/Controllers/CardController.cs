using CardService.UserCardsModule;
using CardService.UserCardsModule.Queries;
using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace CardService.Controllers;

[ApiController]
[Route("cards")]
public class CardController(Counter cardRequestsCounter) : ControllerBase
{
    [HttpGet("actions")]
    public async Task<IActionResult> GetAllowedActions([FromQuery] CardActionsQuery query, [FromServices] IRequestHandler<CardActionsQuery, IEnumerable<string>> handler, 
        CancellationToken cancellationToken)
    {
        cardRequestsCounter.Inc();
        return Ok(await handler.Handle(query, cancellationToken));

    }
}



