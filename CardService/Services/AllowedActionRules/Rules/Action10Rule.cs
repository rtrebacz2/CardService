using CardService.Entities;

namespace CardService.Services.AllowedActionRules.Rules;

public class Action10Rule : ICardActionRule
{
    public string Action=> "ACTION10";
    public bool IsAllowed(CardDetails details) => new[] { CardStatus.ORDERED, CardStatus.INACTIVE, CardStatus.ACTIVE }.Contains(details.CardStatus);
}