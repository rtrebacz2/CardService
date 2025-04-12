using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public class Action9Rule : ICardActionRule
{
    public CardAction Action => "ACTION9";
    public bool IsAllowed(CardDetails details) => true;
}