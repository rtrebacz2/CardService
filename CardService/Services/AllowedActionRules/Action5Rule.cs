using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public class Action5Rule : ICardActionRule
{
    public CardAction Action => "ACTION5";
    public bool IsAllowed(CardDetails details) => details.Type == CardType.CREDIT;
}