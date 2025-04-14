using CardService.Entities;

namespace CardService.Services.AllowedActionRules.Rules;

public class Action6Rule : ICardActionRule
{
    public string Action=> "ACTION6";

    public bool IsAllowed(CardDetails details)
    {
        if (details.IsPinSet == false)
            return false;

        return new[] { CardStatus.ORDERED, CardStatus.INACTIVE, CardStatus.ACTIVE, CardStatus.BLOCKED }.Contains(details.CardStatus);
    }
}