using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public class Action8Rule : ICardActionRule
{
    public CardAction Action => "ACTION8";
    public bool IsAllowed(CardDetails details) => new[] {CardStatus.ORDERED, CardStatus.INACTIVE, CardStatus.ACTIVE, CardStatus.BLOCKED}.Contains(details.Status);
}