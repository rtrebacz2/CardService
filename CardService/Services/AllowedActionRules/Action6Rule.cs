using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public class Action6Rule : ICardActionRule
{
    public CardAction Action => "ACTION6";

    public bool IsAllowed(CardDetails details)
    {
        if(details.IsPinSet == false)
            return false;

        return new[] {CardStatus.ORDERED, CardStatus.INACTIVE, CardStatus.ACTIVE, CardStatus.BLOCKED }.Contains(details.CardStatus);
    }
}