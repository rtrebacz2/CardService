using CardService.Entities;

namespace CardService.Services.AllowedActionRules.Rules;

public class Action8Rule : ICardActionRule
{
    public string Action=> "ACTION8";
    public bool IsAllowed(CardDetails details) => new[] { CardStatus.ORDERED, CardStatus.INACTIVE, CardStatus.ACTIVE, CardStatus.BLOCKED }.Contains(details.CardStatus);
}