using CardService.UserCardsModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace CardService.Controllers;

[ApiController]
[Route("cards")]
public class CardController(IMediator mediator, Counter cardRequestsCounter) : ControllerBase
{
    [HttpGet("actions")]
    public async Task<IActionResult> GetAllowedActions([FromQuery] CardActionsQuery query, CancellationToken cancellationToken)
    {
        cardRequestsCounter.Inc();
        return Ok(await mediator.Send(new CardActionsQuery{UserId = query.UserId, CardNumber = query.CardNumber}));

    }
}



