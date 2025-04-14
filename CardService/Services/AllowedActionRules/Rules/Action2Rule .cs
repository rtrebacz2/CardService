using CardService.Entities;

namespace CardService.Services.AllowedActionRules.Rules;

public class Action2Rule : ICardActionRule
{
    public string Action=> "ACTION2";
    public bool IsAllowed(CardDetails details) => details.CardStatus == CardStatus.INACTIVE;
}