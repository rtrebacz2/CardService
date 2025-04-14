using CardService.Entities;

namespace CardService.Services.AllowedActionRules.Rules;

public class Action13Rule : ICardActionRule
{
    public string Action=> "ACTION13";
    public bool IsAllowed(CardDetails details) => new[] { CardStatus.INACTIVE, CardStatus.ACTIVE }.Contains(details.CardStatus);
}