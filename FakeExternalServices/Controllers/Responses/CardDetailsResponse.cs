using FakeExternalServices.Entities;

namespace FakeExternalServices.Controllers.Responses;

public record CardDetailsResponse(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet)
{
    public static CardDetailsResponse FromCardDetails(CardDetails details)
        => new(details.CardNumber, details.CardType, details.CardStatus, details.IsPinSet);
}