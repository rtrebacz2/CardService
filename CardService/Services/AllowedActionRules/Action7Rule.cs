using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public class Action7Rule : ICardActionRule
{
    public CardAction Action => "ACTION7";

    public bool IsAllowed(CardDetails details)
    {
        return new[] {CardStatus.ORDERED, CardStatus.INACTIVE, CardStatus.ACTIVE, CardStatus.BLOCKED}.Contains(details.Status) && details.IsPinSet;
    }
}