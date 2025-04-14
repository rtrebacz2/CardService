using CardService.Entities;

namespace CardService.Services.AllowedActionRules.Rules;

public class Action1Rule : ICardActionRule
{
    public string Action=> "ACTION1";
    public bool IsAllowed(CardDetails details) => details.CardStatus == CardStatus.ACTIVE;
}