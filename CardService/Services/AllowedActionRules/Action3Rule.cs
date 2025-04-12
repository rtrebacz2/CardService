using CardService.Entities;

namespace CardService.Services.AllowedActionRules;

public class Action3Rule : ICardActionRule
{
    public CardAction Action => "ACTION3";
    public bool IsAllowed(CardDetails details) => true;
}