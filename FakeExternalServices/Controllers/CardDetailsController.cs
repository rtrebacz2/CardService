using FakeExternalServices.Controllers.Responses;
using FakeExternalServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeExternalServices.Controllers;

[Route("[controller]")]
public class CardDetailsController(ICardDetailsService cardDetailsService) : ControllerBase
{
    [HttpGet("{userId}/{cardNumber}")]
    public ActionResult<CardDetailsResponse> Get(string userId, string cardNumber)
    {
        var cardDetails = cardDetailsService.GetCardDetails(userId, cardNumber);

        return cardDetails is null
            ? Problem(detail: $"Card {cardNumber} not found.", statusCode: StatusCodes.Status404NotFound)
            : CardDetailsResponse.FromCardDetails(cardDetails);
    }

    [HttpGet("{userId}")]
    public ActionResult<List<CardDetailsResponse>> Get(string userId)
    {
        var cardDetails = cardDetailsService.GetAllCardDetails(userId);

        return cardDetails.ConvertAll(CardDetailsResponse.FromCardDetails);
    }
}




