using CardService.UserCardsModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CardService.Controllers;

[ApiController]
[Route("cards")]
public class CardController(IMediator mediator) : ControllerBase
{
    [HttpGet("actions")]
    public async Task<IActionResult> GetAllowedActions([FromQuery] CardActionsQuery query, CancellationToken cancellationToken)
    {

        return Ok(await mediator.Send(new CardActionsQuery{UserId = query.UserId, CardNumber = query.CardNumber}));

    }
}



