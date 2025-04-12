using CardService.Controllers.Requests;
using CardService.Entities;
using CardService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardService.Controllers;

[Route("[controller]")]
public class CardController(ICardsService cardsService) : ControllerBase
{
    [HttpGet("actions")]
    public async Task<ActionResult<List<CardAction>>> GetAllowedActions([FromQuery] GetCardActionsQuery query)
    {
        var actions = await cardsService.GetPossibleActionsForUserCard(query.UserId, query.CardNumber);
        return Ok(actions.Select(a => (string)a!));

    }
}



