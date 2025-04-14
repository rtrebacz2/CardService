using CardService.Controllers.Requests;
using CardService.Entities;
using CardService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardService.Controllers;

[Route("[controller]")]
public class CardController(ICardService cardService) : ControllerBase
{
    [HttpGet("actions")]
    public async Task<ActionResult<List<CardAction>>> GetAllowedActions([FromQuery] GetCardActionsQuery query)
    {
        var actions = await cardService.GetPossibleActionsForUserCard(query.UserId, query.CardNumber);
        return Ok(actions.Select(a => (string)a!));

    }
}



