using CardService.Entities;

namespace CardService.Services.AllowedActionRules.Rules;

public class Action12Rule : ICardActionRule
{
    public string Action=> "ACTION12";
    public bool IsAllowed(CardDetails details) => new[] { CardStatus.ORDERED, CardStatus.INACTIVE, CardStatus.ACTIVE }.Contains(details.CardStatus);
}