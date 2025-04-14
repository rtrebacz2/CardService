using CardService.Entities;

namespace CardService.Services.AllowedActionRules.Rules;

public class Action5Rule : ICardActionRule
{
    public string Action=> "ACTION5";
    public bool IsAllowed(CardDetails details) => details.CardType == CardType.CREDIT;
}