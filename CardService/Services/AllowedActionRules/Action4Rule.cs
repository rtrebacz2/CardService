using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public class Action4Rule : ICardActionRule
{
    public CardAction Action => "ACTION4";
    public bool IsAllowed(CardDetails details) => true;
}