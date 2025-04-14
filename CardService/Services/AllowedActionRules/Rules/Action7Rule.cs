using CardService.Entities;

namespace CardService.Services.AllowedActionRules.Rules;

public class Action7Rule : ICardActionRule
{
    public string Action=> "ACTION7";

    public bool IsAllowed(CardDetails details)
    {
        return new[] { CardStatus.ORDERED, CardStatus.INACTIVE, CardStatus.ACTIVE, CardStatus.BLOCKED }.Contains(details.CardStatus) && details.IsPinSet;
    }
}