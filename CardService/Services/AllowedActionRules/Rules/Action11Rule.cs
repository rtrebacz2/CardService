using CardService.Entities;

namespace CardService.Services.AllowedActionRules.Rules;

public class Action11Rule : ICardActionRule
{
    public string Action=> "ACTION11";
    public bool IsAllowed(CardDetails details) => new[] { CardStatus.INACTIVE, CardStatus.ACTIVE }.Contains(details.CardStatus);
}