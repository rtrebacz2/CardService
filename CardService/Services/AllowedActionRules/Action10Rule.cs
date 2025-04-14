using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public class Action10Rule : ICardActionRule
{
    public CardAction Action => "ACTION10";
    public bool IsAllowed(CardDetails details) => new[] { CardStatus.ORDERED, CardStatus.INACTIVE, CardStatus.ACTIVE}.Contains(details.CardStatus);
}