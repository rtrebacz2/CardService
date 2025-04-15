using CardService.Entities;

namespace CardService.Services.AllowedActionRules.Rules;

public class Action7Rule : ICardActionRule
{
    public string Action => "ACTION7";

    public bool IsAllowed(CardDetails details)
    {
        if(details.IsPinSet)
            return new[] { CardStatus.BLOCKED }.Contains(details.CardStatus);

        return new[] { CardStatus.ORDERED, CardStatus.INACTIVE, CardStatus.ACTIVE }.Contains(details.CardStatus);
    }
}